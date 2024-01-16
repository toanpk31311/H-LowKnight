using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
    {
    protected Rigidbody2D rb;
    protected float xInput;
    protected float yInput;
    protected PlayerStateMachine stateMachine;
    protected PlayerControll player;
    private string animBoolName;
    protected float stateTimer;
    protected bool triggelcall;

    public PlayerState(PlayerControll _player, PlayerStateMachine _PlayerSM,string animBoolName)
    {
        this.player= _player;
        this.animBoolName= animBoolName;
        this.stateMachine = _PlayerSM; 
    }
    public virtual void Enter()
    {
      player.anim.SetBool(animBoolName, true);
        rb= player.rb;
        triggelcall = false;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity",rb.velocity.y);
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void AnimationFinishTrigger()
    {
        triggelcall = true;
    }


}

