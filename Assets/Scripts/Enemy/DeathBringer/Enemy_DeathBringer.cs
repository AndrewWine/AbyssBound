using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy_DeathBringer : Enemy
{
    [Header("Spell cast details")]
    [SerializeField] private GameObject spellPrefab;
    private float lastTimeCast;
    [SerializeField] private float spellCastCooldown;
    private Transform player;


    [Header("Teleport details")]
    [SerializeField] private BoxCollider2D arena;
    [SerializeField] private Vector2 surroundingChecksize;

    private void OnEnable()
    {
        ExitTeleport.NotifyFindPlace += FindPosition;
    }

    private void OnDisable()
    {
        ExitTeleport.NotifyFindPlace -= FindPosition;
    }

    public override bool CanAttack()
    {
        // Kiểm tra nếu đủ thời gian chờ giữa các lượt tấn công
        base.CanAttack(); return true;
    }

    public override void Damage()
    {
        base.Damage();
    }

    public override void Flip()
    {
        base.Flip();
    }

    public override void SetVelocityX(float velocity)
    {
        base.SetVelocityX(velocity);
    }

    public override void SetVelocityY(float velocity)
    {
        base.SetVelocityY(velocity);
    }

    public override void SetVelocityZero()
    {
        base.SetVelocityZero();
    }

    public override void TakeDamage()
    {
        base.TakeDamage();
    }

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.Find("Player").GetComponent<Transform>();

    }

    protected override void CheckObject()
    {
        base.CheckObject();
    }

    protected override void Death()
    {
        // Chỉ thực hiện khi enemy chưa chết
        if (!isDead && BeingHit.CurrentHP <= 0)
        {
            isDead = true; // Đánh dấu enemy đã chết
            enemystateMachine.ChangeState(entity.enemyDBDeathState);
            myDropSystem.GenerateDrop(); // Gọi phương thức rơi đồ
            isDeath?.Invoke();
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - GroundBelow().distance));
        Gizmos.DrawWireCube(transform.position, surroundingChecksize);
    }

    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);

        transform.position = new Vector3(x, y);
        transform.position = new Vector3(transform.position.x, transform.position.y - GroundBelow().distance); //+ (cd.size.y/2)
        if (SomethingIsAround())
        {
            Debug.Log("Looking for new position");
            FindPosition();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    private RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, enemyData.whatIsGround);
    private bool SomethingIsAround() => Physics2D.BoxCast(transform.position, surroundingChecksize, 0, Vector2.zero, enemyData.whatIsGround);

    protected override void CheckCountAttack()
    {
        base.CheckCountAttack();
    }

    public void CastSpell()
    {
        Vector3 spellPosition =  new Vector3(player.transform.position.x, player.transform.position.y +1);
        GameObject newSpell = Instantiate(spellPrefab,spellPosition,Quaternion.identity);
        //newSpell.GetComponent<DeathBringerSpellController>().set
    }
}
