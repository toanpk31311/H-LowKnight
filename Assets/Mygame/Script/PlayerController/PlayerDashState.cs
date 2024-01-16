using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerControll _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDurarion;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()

    {
        base.Update();
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        if (stateTimer < 0) {
            
            stateMachine.ChangeState(player.PlayerIdleState);

            if (!player.isGroundDetected() && player.isWallDetected())
            {
                stateMachine.ChangeState(player.slideState);
            }
                }
    }
}
