using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrimMoveState : EnemyState
{
    GrimEnermy enemy;
    public GrimMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,GrimEnermy _enermy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enermy;  
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDr, enemy.rb.velocity.y);
        if (enemy.isWallDetected()||!enemy.isGroundDetected()) 
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
