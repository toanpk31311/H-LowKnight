using UnityEngine;

public class BlackBugDeathState : State
{


    protected BlackBugEnemy blackBugEnemy;

    private float deathAnimTime;

    public BlackBugDeathState(Entities _entitiesBase, StateMachine _stateMachine, string _animBoolName, BlackBugEnemy blackBugEnemy) : base(_entitiesBase, _stateMachine, _animBoolName)
    {
        this.blackBugEnemy = blackBugEnemy;
    }

    public override void Enter()
    {
        base.Enter();

        blackBugEnemy.SetVelocity(0, 0);
        deathAnimTime = blackBugEnemy.deathClip.length;
    }

    public override void Exit()
    {
        base.Exit();

        blackBugEnemy.animator.gameObject.SetActive(false);
        blackBugEnemy.GetCollider().enabled = false;
        blackBugEnemy.SetDeathSprite(true);
    }

    public override void Update()
    {
        base.Update();
        deathAnimTime -= Time.deltaTime;
        
        if (deathAnimTime < 0)
        {
            stateMachine.ExitState(stateMachine.currentState);
        }
    }


}
