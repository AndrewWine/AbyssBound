using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public EnemyState enemyState;
    public Animator animator;
    public Rigidbody2D RB;
    public bool isWall;
    public bool isGrounded;
    public int FacingDirection;
    public LayerMask playerLayer; // LayerMask for detecting the player

    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;

    [Header("Transform")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public Transform wallCheck;


    [Header("Scriptable Objects")]
    [SerializeField] public EnemyData enemyData;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        RB = GetComponent<Rigidbody2D>();


        // Gán EntityData dựa trên EntityType
        FacingDirection = transform.localScale.x > 0 ? 1 : -1;
    }

    protected virtual void Start()
    {

    }    
    protected virtual void Update()
    {
        // Ground check
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, enemyData.groundCheckDistance, enemyData.whatIsGround);
        Debug.Log("Is Grounded: " + isGrounded);

        // Wall check
        isWall = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, enemyData.WallCheckDistance, enemyData.whatIsGround);
        Debug.Log("Is Wall: " + isWall + " FacingDirection: " + FacingDirection);
    }
    public virtual void Flip()
    {
        // Đảo chiều FacingDirection
        FacingDirection *= -1;
        // Lật đối tượng bằng cách thay đổi góc quay (rotation) theo trục Y
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 180f;
        transform.eulerAngles = rotation;
    }

    private void OnDrawGizmos()
    {
        // Set Gizmo color
        Gizmos.color = Color.yellow;

        // Draw ground check ray
        Vector3 groundCheckPosition = groundCheck.position;
        Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector3.down * enemyData.groundCheckDistance);

        // Draw wall check ray
        Vector3 wallCheckPosition = wallCheck.position;
        Gizmos.DrawLine(wallCheckPosition, wallCheckPosition + Vector3.right * FacingDirection * enemyData.WallCheckDistance);

        // Perform raycast to check for player
         enemyState.hit = Physics2D.Raycast(wallCheckPosition, Vector2.right * FacingDirection, enemyData.WallCheckDistance, playerLayer);
        Debug.Log("Dathayplayer" +  enemyState.hit);

        // Check if the raycast hit the player
        if (enemyState.hit.collider != null)
        {
            enemyState.PlayerCheck = true;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(wallCheckPosition, enemyState.hit.point);
        }
        else
        {
            // Draw a red line if no player is detected
            Gizmos.color = Color.red;
            Gizmos.DrawLine(wallCheckPosition, wallCheckPosition + Vector3.right * FacingDirection * enemyData.WallCheckDistance);
        }
    }

    public virtual void SetVelocityZero()
    {
        RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public virtual void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, 0);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public virtual void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

}
