using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimStunState : EnemyState
{
    GrimEnermy enemy;
    public GrimStunState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,GrimEnermy enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy= enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);
        stateTimer = enemy.stunDuration;
        rb.velocity = new Vector2(-enemy.facingDr * enemy.stunDirection.x, enemy.stunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.idleState);
    }
}
