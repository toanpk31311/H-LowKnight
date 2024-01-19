using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimEnermy : Enemy
{
    #region States
    public GrimIdleState idleState { get; private set; }
    public GrimMoveState moveState { get; private set; }
    public GrimBattleState battleState { get; private set; }
    public GrimAtckState attackState { get; private set; }
    public GrimStunState stunState { get; private set; }    


    #endregion
    protected override void Awake()
    {
        base.Awake();
       
        idleState = new GrimIdleState(this, stateMachine, "Idle", this);
        moveState = new GrimMoveState(this, stateMachine, "Move", this);
        //battleState = new GrimBattleState(this, stateMachine, "BatleState", this);
        battleState = new GrimBattleState(this, stateMachine, "Move", this);
        attackState = new GrimAtckState(this, stateMachine, "Attack", this);
        stunState = new GrimStunState(this, stateMachine, "Stun", this);
    }


    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();  
        if (Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeState(stunState);
        }
    }
    public override bool CanBeStunned()
    {
       if(base.CanBeStunned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
       return false;
    }

}
