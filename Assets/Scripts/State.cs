using UnityEngine;

public class State
{


    protected StateMachine stateMachine;
    protected Entities entitiesBase;
    protected Rigidbody2D rb;

    private string animBoolName;

    protected float stateTimer;
    protected float SecondStateTimer;
    protected bool triggerCalled;


    public State(Entities _entitiesBase, StateMachine _stateMachine, string _animBoolName)
    {
        this.entitiesBase = _entitiesBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = entitiesBase.rgbody;
        entitiesBase.animator.SetBool(animBoolName, true);

    }

    public virtual void Exit()
    {
        entitiesBase.animator.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }


}
