using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine StateMachine { get; private set; }

    [Header("States")]
    public IdleState enemyIdleState;
    public AttackState enemyattackState;
    public HitState enemyHitState;
    public DeathState enemyDeathState;
    public WalkState enemyWalkState;
    public ChasingState enemyChasingState;
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

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
