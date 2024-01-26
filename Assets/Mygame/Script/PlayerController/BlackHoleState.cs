using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleState : PlayerState
{
    private float flyTime = .25f;
    private bool skillUsed;


    private float defaultGravity;
    public BlackHoleState(Player _player, PlayerStateMachine _PlayerSM, string animBoolName) : base(_player, _PlayerSM, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravity = player.rb.gravityScale;

        skillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = defaultGravity;
        PlayerManager.instance.player.MakeTransprent(false);
        
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer > 0)
            rb.velocity = new Vector2(0, 15);

        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0, -.1f);
            if (!skillUsed)
            {
                if (player.Skill.blackHole.CanUseSkill())
                   
                    skillUsed = true;
            }
        }
        if (player.Skill.blackHole.SkillCompleted())
        {
            stateMachine.ChangeState(player.PlayerAirState);
        }
    }
}
