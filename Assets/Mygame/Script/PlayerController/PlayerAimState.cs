using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(Player _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Skill.Sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();

        if (Input.GetKeyUp(KeyCode.R))
            stateMachine.ChangeState(player.PlayerIdleState);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePosition.x && player.facingDr == 1)
            player.Flip();
        else if (player.transform.position.x < mousePosition.x && player.facingDr == -1)
            player.Flip();
    }

}
