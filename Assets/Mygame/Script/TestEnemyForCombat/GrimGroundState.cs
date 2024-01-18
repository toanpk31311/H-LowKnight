using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimGroundState : EnemyState
{
    protected GrimEnermy enemy;
    protected Transform player;
    public GrimGroundState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,GrimEnermy enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
       
        base.Enter();
        player = GameObject.Find("Player").transform;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected()) {
            Debug.Log("Cos y nghia");
            stateMachine.ChangeState(enemy.battleState);
            
        }
    }
}
