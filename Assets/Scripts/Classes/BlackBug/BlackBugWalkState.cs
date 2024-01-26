using UnityEngine;

public class BlackBugWalkState : State
{


    protected BlackBugEnemy blackBugEnemy;

    private float walkAnimTime;


    public BlackBugWalkState(Entities _entitiesBase, StateMachine _stateMachine, string _animBoolName, BlackBugEnemy blackBugEnemy) : base(_entitiesBase, _stateMachine, _animBoolName)
    {
        this.blackBugEnemy = blackBugEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        
        walkAnimTime = blackBugEnemy.walkClip.length;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        walkAnimTime -= Time.deltaTime;
        blackBugEnemy.SetVelocity(blackBugEnemy.moveSpeed * blackBugEnemy.facingDirection, 0);

        if ((blackBugEnemy.IsWallDetected() || !blackBugEnemy.IsGroundDetected()) && walkAnimTime < 0)
        {
            stateMachine.ChangeState(blackBugEnemy.turnState);
        }

        if (blackBugEnemy.health == 0)
        {
            stateMachine.ChangeState(blackBugEnemy.deathState);
        }
    }


}
