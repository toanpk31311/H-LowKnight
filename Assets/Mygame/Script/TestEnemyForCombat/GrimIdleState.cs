using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimIdleState : EnemyState
{   GrimEnermy enemy;
    public GrimIdleState(Enemy _enemybase, EnemyStateMachine _stateMachine, string _animBoolName, GrimEnermy _enemy) : base(_enemybase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
