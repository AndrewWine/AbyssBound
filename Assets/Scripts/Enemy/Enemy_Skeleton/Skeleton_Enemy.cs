using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Enemy : Enemy
{
    public EntityBlackboard blackBoard;

    public override bool CanAttack()
    {
        return base.CanAttack();
    }

    public override bool CanBeStunned()
    {
        // Kiểm tra các điều kiện stun trong lớp cha
        if (base.CanBeStunned())
        {
            enemystateMachine.ChangeState(blackBoard.enemystunState); // Chuyển sang trạng thái stun
            return true;
        }
        return false;
    }

    public override void CloseCounterAttackWindow()
    {
        base.CloseCounterAttackWindow();
    }

    public override void Damage()
    {
        base.Damage();
    }

    public override void Flip()
    {
        base.Flip();
    }

    public override void OpenCounterAttackWindow()
    {
        base.OpenCounterAttackWindow();
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
        enemystateMachine.ChangeState(blackBoard.enemyHitState);
        base.TakeDamage();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void CheckObject()
    {
        base.CheckObject();
    }

    protected override void Death()
    {

        if (!isDead && BeingHit.CurrentHP <= 0)
        {
            isDead = true; 
            enemystateMachine.ChangeState(blackBoard.enemyDeathState);
            myDropSystem.GenerateDrop(); 
            isDeath?.Invoke();
        }
    }

    protected override IEnumerator HitKnockback()
    {
        return base.HitKnockback();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

  

    protected override void Update()
    {
        base.Update();
    }
}
