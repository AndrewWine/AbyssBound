using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SwordSkill_Controller : MonoBehaviour
{
    public Action CallCatchState;

    [Header("Component")]
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private ObjectPool<SwordSkill_Controller> swordPool;
    [SerializeField] public Transform playerPos;
    [Header("Movement")]
    public float speed; // Tốc độ thanh kiếm (nếu cần sử dụng)

    [Header("Lifetime Management")]
    private float lifetime; // Thời gian tồn tại của thanh kiếm
    private float lifetimeTimer; // Bộ đếm thời gian để theo dõi thời gian tồn tại
    public bool canRotate;

    [Header("Bounce")]
    public bool isBouncing = true;
    public int amountOfBounce;
    public List<Transform> enemyTarget;
    private int targetIndex;
    [SerializeField] float bounceSpeed = 20;
    [SerializeField] float ReturnSpeed = 40;
    public bool isReturning = false;
    private Transform currentTransform;
    private float circleColliderRadius; // Biến để lưu kích thước của CircleCollider2D

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        Debug.Log("SwordSkill_Controller Initialized");

        // Lấy kích thước của CircleCollider2D
        if (cd != null)
        {
            circleColliderRadius = cd.radius;
            Debug.Log("Kích thước CircleCollider2D là: " + circleColliderRadius);
        }
        else
        {
            Debug.LogError("CircleCollider2D không tồn tại trên đối tượng này.");
        }

        canRotate = true;
        playerPos = FindObjectOfType<Player>().transform;
        if (playerPos == null)
        {
            Debug.Log("Lỗi Player không tồn tại");
        }
        currentTransform = this.transform;
    }

    public void SetupSword(Vector2 direction, float gravityScale, float lifetime)
    {
        rb.velocity = direction; // Set sword velocity
        rb.gravityScale = gravityScale; // Set gravity scale
        this.lifetime = lifetime; // Set lifetime
        lifetimeTimer = lifetime; // Reset lifetime timer
        rb.isKinematic = false; // Reset kinematic state
        cd.enabled = true; // Enable collider
        rb.constraints = RigidbodyConstraints2D.None; // Allow movement and rotation
        canRotate = true;
        isReturning = false;
        // Reset bouncing-related properties
        enemyTarget.Clear(); // Clear the list of targets
        amountOfBounce = 0; // Reset bounce amount
        isBouncing = true; // Enable bouncing again
    }

    private void Update()
    {
        lifetimeTimer += Time.deltaTime;
        if(lifetimeTimer > 10)
        {
            ReturnToPool();
        }
        if (canRotate)
        {
            animator.Play("SwordFlip");
        }

        if (isReturning)
        {
            animator.Play("SwordFlip");
            // Move sword towards playerPos
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, ReturnSpeed * Time.deltaTime);

            // Check if sword is close enough to the playerPos
            if (Vector2.Distance(transform.position, playerPos.position) < 0.2f)
            {
                Debug.Log("Sword about to trigger CallCatchState.");
                CallCatchState?.Invoke();
                Debug.Log("CallCatchState invoked.");
                ReturnToPool();
            }
        }
        lifetime = Time.deltaTime;
        if (lifetime > 10)
        {
            ReturnToPool();
        }
        // Giảm thời gian tồn tại

        // Xử lý logic bounce
        if (isBouncing && enemyTarget.Count > 0 && amountOfBounce > 0) // Thêm điều kiện kiểm tra amountOfBounce
        {
            rb.transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < 1f)
            {
                targetIndex++;
                amountOfBounce--; // Giảm số lần bounce mỗi khi đến được enemy

                if (amountOfBounce <= 0)
                {
                    isBouncing = false; // Dừng bounce khi đủ số lần
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(currentTransform.position, circleColliderRadius); // Sử dụng kích thước collider đã lấy
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
        Debug.Log("Sword collided with: " + collision.name);

        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBouncing && enemyTarget.Count <= 0) // Find new enemies if not already bouncing
            {
                FindNewBounceTargets();
            }
        }
        isReturning = true;
        
    }

    private void FindNewBounceTargets()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10); // Find enemies in range

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                enemyTarget.Add(hit.transform);
            }
        }

        // Set amount of bounce based on the number of targets found
        amountOfBounce = enemyTarget.Count * 2;
    }

  

    private void ReturnToPool()
    {
        gameObject.SetActive(false); // Ẩn thanh kiếm
        if (swordPool != null)
        {
            swordPool.ReturnToPool(this); // Trả thanh kiếm về pool nếu có
        }
    }

    public void SetPool(ObjectPool<SwordSkill_Controller> pool)
    {
        swordPool = pool; // Thiết lập pool cho thanh kiếm
    }
}
