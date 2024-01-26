using UnityEngine;

public class BlackBugWalkState : State
{


    protected BlackBugEnemy blackBugEnemy;

    public BlackBugWalkState(Entities _entitiesBase, StateMachine _stateMachine, string _animBoolName, BlackBugEnemy blackBugEnemy) : base(_entitiesBase, _stateMachine, _animBoolName)
    {
        this.blackBugEnemy = blackBugEnemy;
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
        blackBugEnemy.SetVelocity(blackBugEnemy.moveSpeed * blackBugEnemy.facingDirection, 0);

        if (blackBugEnemy.IsWallDetected() || !blackBugEnemy.IsGroundDetected()) 
        {
            stateMachine.ChangeState(blackBugEnemy.turnState);
        }

        if (blackBugEnemy.health == 0)
        {
            stateMachine.ChangeState(blackBugEnemy.deathState);
        }
    }


}
