using UnityEngine;
using System.Collections;  
public class CloneSkill_Controller : MonoBehaviour
{
    [Header("Component")]
    public PlayerData playerData;
    private SpriteRenderer sr;
    private Animator animator;
    public CloneAttackTrigger cloneAttackTrigger;
    [Header("Other variables")]
    [SerializeField] private float cloneDuration; // Duration of the clone's existence
    [SerializeField] private float colorLoosingSpeed;
    private float cloneTimer = 3;

    [Header("Transform")]
    private Transform closestEnemy;


    private ObjectPool<CloneSkill_Controller> clonePool;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Reduce the clone's duration
        cloneTimer -= Time.deltaTime;

        // Return clone to pool if time is up
        if (cloneTimer > 0)
        {
            sr.color = new Color(1, 1, 1, Mathf.Clamp01(sr.color.a - (Time.deltaTime * colorLoosingSpeed)));
        }
        else
            ReturnToPool();
    }

    // Initialize clone and set the object pool
    public void SetupClone(Transform _newTransform, float _cloneDuration, bool _canAttack)
    {
        cloneTimer = _cloneDuration;  // Set the clone's lifetime

        // Ensure the clone's attack animation is set based on click count
        if (_canAttack)
        {
            if (playerData.CountClick == 1)
                animator.Play("Clone_Attack1");
            else if (playerData.CountClick == 2)
                animator.Play("Clone_Attack2");
            else if (playerData.CountClick == 3)
                animator.Play("Clone_Attack3");
        }

        FaceClosestTarget();
    }

    private void ReturnToPool()
    {
        // Reset clone parameters if necessary
        sr.color = new Color(1, 1, 1, 1); // Ensure clone is fully visible
        clonePool.ReturnToPool(this); // Return clone to pool
        Debug.Log("Clone has been returned to the pool.");
    }

    // Trigger attack and face closest enemy
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(cloneAttackTrigger.attackCheck.position, cloneAttackTrigger.attackRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        float closestDistance = Mathf.Infinity;
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }

    // Wait 1-2 seconds after animation ends before returning clone to pool
    private IEnumerator ReturnToPoolAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }

    private void AnimationTrigger()
    {
        // Delay before returning to pool
        StartCoroutine(ReturnToPoolAfterDelay(0.5f)); // Adjust delay as needed (1.5 seconds here)
    }

    private void OnDrawGizmos()

    {
        Gizmos.color = Color.yellow;

        // Draw ground check ray
       
        
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(cloneAttackTrigger.attackCheck.position, cloneAttackTrigger.attackRadius);
    }

    //Set the pool reference (called by CloneAttack)
    public void SetPool(ObjectPool<CloneSkill_Controller> pool)
    {
        clonePool = pool;
    }


}
