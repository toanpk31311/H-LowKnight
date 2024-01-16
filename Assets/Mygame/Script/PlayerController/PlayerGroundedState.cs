using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(PlayerControll _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
    {

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAtck);

        }
        if (!player.isGroundDetected())
        {
            stateMachine.ChangeState(player.PlayerAirState);
        }
       if(Input.GetKeyDown(KeyCode.Space)&&player.isGroundDetected()) {
            stateMachine.ChangeState(player.PlayerJumpState);
        } 
    }
}
