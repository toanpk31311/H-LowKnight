using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttack : PlayerState
{
    public PlayerCounterAttack(PlayerControll _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        player.SetZeroVelocity();
        base.Update();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if(hit.GetComponent<Enemy>().CanBeStunned())
                {

                    stateTimer = 10;
                    player.anim.SetBool("SuccessCounterAttack", true);
                }
            }
        }
        if (stateTimer < 0 || triggelcall)
            stateMachine.ChangeState(player.PlayerIdleState);
    }
    }
