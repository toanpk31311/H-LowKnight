using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerControll _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        rb.velocity= new Vector2(0,0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(xInput == player.facingDr&&player.isWallDetected())
         return;
        
        if (xInput != 0&& !player.isBusy)
        {
            stateMachine.ChangeState(player.MoveState);
        }


    }
}



   

