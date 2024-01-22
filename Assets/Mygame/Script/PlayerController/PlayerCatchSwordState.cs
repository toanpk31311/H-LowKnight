using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;

    public PlayerCatchSwordState(Player _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
    {
    }
   
    public override void Enter()
    {
        base.Enter();
        sword = player.sword.transform;
        if (player.transform.position.x > sword.position.x && player.facingDr == 1)
            player.Flip();
        else if (player.transform.position.x < sword.position.x && player.facingDr == -1)
            player.Flip();
        rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDr, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .1f);
    }

    public override void Update()
    {
        base.Update();
        if (triggelcall)
            stateMachine.ChangeState(player.PlayerIdleState);
    }
}
