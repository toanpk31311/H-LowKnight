using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAtck : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow=2;
    public PlayerPrimaryAtck(PlayerControll _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(comboCounter >2 || Time.time >= lastTimeAttacked+ comboWindow) {
            comboCounter = 0;
        }
        player.anim.SetInteger("ComboCounter", comboCounter);
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttacked= Time.time;
    }

    public override void Update()
    {   if (stateTimer<0)
        {
            rb.velocity= new Vector2(0,0);
        }
        base.Update();
        if (triggelcall)
        {
            stateMachine.ChangeState(player.PlayerIdleState);
        }
    }
}
