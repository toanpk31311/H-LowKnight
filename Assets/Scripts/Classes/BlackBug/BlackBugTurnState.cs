using UnityEngine;

public class BlackBugTurnState : State
{


    protected BlackBugEnemy blackBugEnemy;

    private float turnAnimTime;

    public BlackBugTurnState(Entities _entitiesBase, StateMachine _stateMachine, string _animBoolName, BlackBugEnemy blackBugEnemy) : base(_entitiesBase, _stateMachine, _animBoolName)
    {
        this.blackBugEnemy = blackBugEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        blackBugEnemy.SetVelocity(0, 0);
        turnAnimTime = blackBugEnemy.turnClip.length;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        turnAnimTime -= Time.deltaTime;
        
        if (turnAnimTime < 0)
        {
            blackBugEnemy.Flip();
            stateMachine.ChangeState(blackBugEnemy.walkState);
        }
    }


}
