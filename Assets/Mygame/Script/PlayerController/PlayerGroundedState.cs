using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
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
        if (Input.GetKey(KeyCode.Mouse0))
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
        if (Input.GetKeyDown(KeyCode.Mouse1))
            stateMachine.ChangeState(player.CounterAttack);
        if (Input.GetKeyDown(KeyCode.R)&&HasNoSword())
           
            stateMachine.ChangeState(player.AimState);
        if (Input.GetKeyDown(KeyCode.F)){
            stateMachine.ChangeState(player.blackHoleState);
        }
    }
    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }

        player.sword.GetComponent<SwordSkillController>().ReturnSword();
        return false;
    }
}
