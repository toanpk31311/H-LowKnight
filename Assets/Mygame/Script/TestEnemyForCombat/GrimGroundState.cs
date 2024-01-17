using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimGroundState : EnemyState
{
    protected GrimEnermy enemy;
    public GrimGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,GrimEnermy enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
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
        if (enemy.IsPlayerDetected()) {
            stateMachine.ChangeState(enemy.battleState);
            
        }
    }
}
