using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Component")]
    public EnemyBlackBoard entity;
    public EnemyStateMachine enemystateMachine;
    public EnemyData enemyData;
    public UnitHP BeingHit;
    public EntityFX fx { get; private set; }
    

   
    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;
    public float distanceBattle;

    public void AnimationFinishTrigger()
    {
        enemystateMachine.CurrentState.AnimationFinishTrigger();
    }
    protected virtual void Awake()
    {
        fx = GetComponent<EntityFX>();
        entity.animator = GetComponentInChildren<Animator>();
        entity.RB = GetComponent<Rigidbody2D>();
        // Gán EntityData dựa trên EntityType
        entity.FacingDirection = transform.localScale.x > 0 ? 1 : -1;

        BeingHit.BeingHit += TakeDamage;
    }

    private void Disable()
    {
        BeingHit.BeingHit -= TakeDamage;
    }
    protected virtual void Update()
    {
        if(BeingHit.CurrentHP <= 0)
        {
            enemystateMachine.ChangeState(entity.enemyDeathState);
        }
        CheckObject();
        CanAttack();
    }

    public virtual void TakeDamage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        Debug.Log(gameObject.name + "was damaged!");
    }
    
    protected virtual IEnumerator HitKnockback()
    {
        entity.isKnocked  = true;
        entity.RB.velocity = new Vector2(entity.knockbackDirection.x * -entity.FacingDirection,entity.knockbackDirection.y);
        yield return new WaitForSeconds(entity.knockbackDuration);
        entity.isKnocked = false;
    }

    public bool CanAttack()
    {
        if (Time.time >= enemyData.lastTimeAttacked + enemyData.attackCooldown)
        {
            enemyData.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }

    public virtual void OpenCounterAttackWindow()
    {
        entity.canBeStunned = true;
        entity.counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        entity.canBeStunned = false;
        entity.counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if(!entity.canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public virtual void Flip()
    {
        // Đảo chiều FacingDirection
        entity.FacingDirection *= -1;
        // Lật đối tượng bằng cách thay đổi góc quay (rotation) theo trục Y
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 180f;
        transform.eulerAngles = rotation;
       
    }

    protected virtual void CheckObject()
    {
        // Perform OverlapCircle to check for playerPos
        Collider2D playerCollider = Physics2D.OverlapCircle(entity.RB.position, 10f, entity.playerLayer);
        if (playerCollider != null)
        {
            // Nếu phát hiện playerPos, lấy Transform của playerPos
            entity.targetPlayer = playerCollider.transform;

            // Di chuyển nhân vật về phía playerPos
            entity.playerDetected = true;
        }
        else entity.playerDetected = false;
        // Log the result

        // Ground check
        entity.isGrounded = Physics2D.Raycast(entity.groundCheck.position, Vector2.down, enemyData.groundCheckDistance, enemyData.whatIsGround);

        // Wall check
        entity.isWall = Physics2D.Raycast(entity.wallCheck.position, Vector2.right * entity.FacingDirection, enemyData.WallCheckDistance, enemyData.whatIsGround);

        entity.isPlayer = Physics2D.Raycast(entity.PlayerCheck.position, Vector2.right * entity.FacingDirection, enemyData.PlayerCheckDistance, enemyData.whatIsPlayer);

    }
    protected virtual void OnDrawGizmos()
    {
        // Set Gizmo color
        Gizmos.color = Color.blue;

        // Draw ground check ray
        Vector3 groundCheckPosition = entity.groundCheck.position;
        Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector3.down * enemyData.groundCheckDistance);
        // Draw wall check ray  
        Gizmos.DrawLine(entity.wallCheck.position, entity.wallCheck.position + Vector3.right * entity.FacingDirection * enemyData.WallCheckDistance);
        // Draw the overlap circle to visualize detection range
        Gizmos.color = Color.yellow; // Change color for the overlap circle
        Gizmos.DrawWireSphere(entity.RB.position, 10f); // Draw wire sphere to visualize the overlap range
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(entity.attackCheck.position, enemyData.attackCheckRadius);
        //Draw playerPos check ray
        Gizmos.color = Color.red;
        Gizmos.DrawLine(entity.PlayerCheck.position, entity.PlayerCheck.position + Vector3.right * entity.FacingDirection * enemyData.PlayerCheckDistance);
    }

    public virtual void SetVelocityZero()
    {
      
        entity.RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public virtual void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, 0);
        entity.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public virtual void SetVelocityY(float velocity)
    {
        if(entity.isKnocked)
            return;
        workspace.Set(CurrentVelocity.x, velocity);
        entity.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
}
