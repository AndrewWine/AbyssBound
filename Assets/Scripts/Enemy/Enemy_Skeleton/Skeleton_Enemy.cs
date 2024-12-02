using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Enemy : Enemy
{
    public override bool CanBeStunned()
    {
        // Kiểm tra các điều kiện stun trong lớp cha
        if (base.CanBeStunned())
        {
            enemystateMachine.ChangeState(entity.enemystunState); // Chuyển sang trạng thái stun
            return true;
        }
        return false;
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
    }

    protected override void Death()
    {
        base.Death();
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
