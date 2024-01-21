using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GrimMoveState : GrimGroundState
{
    public GrimMoveState(GroundOnlyEnemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, GrimEnermy enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
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
