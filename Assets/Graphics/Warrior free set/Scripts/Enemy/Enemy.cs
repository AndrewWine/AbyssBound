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
    private ItemDrop myDropSystem;

    public EntityFX fx { get; private set; }


    private bool isDead = false; // Cờ kiểm tra trạng thái chết

    protected float lastStunTime;  // Thời gian bị stun gần nhất
    public float stunCooldown = 2f; // Thời gian hồi trước khi có thể bị stun lại


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
        myDropSystem = GetComponent<ItemDrop>();
        
    }

    
    protected virtual void Update()
    {
        Death();
        CheckObject();
        CanAttack();
    }

    private void Death()
    {
        // Chỉ thực hiện khi enemy chưa chết
        if (!isDead && BeingHit.CurrentHP <= 0)
        {
            isDead = true; // Đánh dấu enemy đã chết
            enemystateMachine.ChangeState(entity.enemyDeathState);
            myDropSystem.GenerateDrop(); // Gọi phương thức rơi đồ
        }
    }

    public virtual void TakeDamage()
    {
        enemystateMachine.ChangeState(entity.enemyHitState);
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
        if (!entity.canBeStunned && entity.isPlayer) // Chỉ mở nếu chưa kích hoạt và phát hiện người chơi
        {
            entity.canBeStunned = true;
            entity.counterImage.SetActive(true);
        }
    }

    public virtual void CloseCounterAttackWindow()
    {
        entity.canBeStunned = false;
        entity.counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        // Không thể bị stun nếu đang trong trạng thái stun
        if (enemystateMachine.CurrentState == entity.enemystunState)
            return false;

        // Thời gian hồi trước khi có thể bị stun lại
        if (Time.time < lastStunTime + stunCooldown)
            return false;

        // Các điều kiện khác (ví dụ: chỉ stun khi nhận sát thương từ counter)
        if (!entity.canBeStunned)
            return false;

        // Cập nhật thời gian bị stun gần nhất
        lastStunTime = Time.time;
        return true;
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
