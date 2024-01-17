using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimEnermy : Enemy
{
    #region States
    public GrimIdleState idleState { get; private set; }
    public GrimMoveState moveState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new GrimIdleState(this, stateMachine, "Idle", this);
        moveState = new GrimMoveState(this, stateMachine, "Move", this);
    }


    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

}
