using UnityEngine;

public class BlackBugEnemy : GroundEnemy
{


    #region States
    public BlackBugWalkState walkState { get; private set; }
    public BlackBugTurnState turnState { get; private set; }
    public BlackBugDeathState deathState { get; private set; }
    #endregion

    public AnimationClip turnClip { get; private set; }
    public AnimationClip deathClip { get; private set; }

    protected override void Awake()
    {
        base.Awake();
       
        walkState = new BlackBugWalkState(this, stateMachine, "Walk", this);
        turnState = new BlackBugTurnState(this, stateMachine, "Turn", this);
        deathState = new BlackBugDeathState(this, stateMachine, "Death", this);

        turnClip = FindAnimation("Turn");
        deathClip = FindAnimation("Death");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(turnState);
    }

    protected override void Update()
    {
        base.Update();
    }


}
