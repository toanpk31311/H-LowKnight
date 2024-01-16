using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    // Start is called before the first frame update
    public PlayerWallSlideState(PlayerControll _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
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
        if(xInput!= 0&& player.facingDr != xInput)
        {

            stateMachine.ChangeState(player.PlayerIdleState);  
        }
        if (yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 1f);
        }
        else 
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);
        }
       
        if (player.isGroundDetected()) 
        {
            stateMachine.ChangeState(player.PlayerIdleState);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }
    }
}
