using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : Entity    
{
    public bool isBusy { get; private set; }
    [Header("Attack infor")]
    public float[] attackMovement;
    public float counterAttackDuration = .2f;


    [Header("Move infor")]
    
    public float moveSpeed = 2f;
    public float jumpForce = 120f;
    
    [Header("Dash Infor")]
    [SerializeField] private float dashCollDown;
    private float usedashTimer;
    public float dashSpeed;
    public float dashDurarion;
    public float dashDir {  get; private set; }  
    #region State
    public PlayerStateMachine StateMachine
    {
        get; private set;
    }
    public PlayerIdleState PlayerIdleState { get; private set; }    
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState PlayerJumpState { get; private set; }    
    public PlayerAirState PlayerAirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState slideState { get; private set; }
    public PlayerWallJump wallJump {  get; private set; }   
    public PlayerPrimaryAtckState primaryAtck { get; private set; }

    public PlayerCounterAttack CounterAttack { get; private set; }
    #endregion
    protected override void Awake()
    {   base.Awake();
        StateMachine = new PlayerStateMachine();
        PlayerIdleState = new PlayerIdleState(this,StateMachine,"Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        PlayerJumpState= new PlayerJumpState(this, StateMachine, "jump");
        PlayerAirState = new PlayerAirState(this, StateMachine, "jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        slideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        wallJump = new PlayerWallJump(this, StateMachine, "jump");
        primaryAtck = new PlayerPrimaryAtckState(this, StateMachine, "Atack");
        CounterAttack = new PlayerCounterAttack(this, StateMachine, "CounterAttack");

    }
    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(PlayerIdleState);
    }
    protected override void Update()
    {
       base .Update();
        StateMachine.curentState.Update();
        CheckForDashInput();
    }
   

    
    
    private void CheckForDashInput()
    {   
        usedashTimer-= Time.deltaTime;
        if (isWallDetected())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && usedashTimer < 0)
        {
            usedashTimer = dashCollDown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir==0)
            {
                dashDir = facingDr;
            }
            StateMachine.ChangeState(DashState);
        }
            
    }
    public void AniamationTrigger() => StateMachine.curentState.AnimationFinishTrigger();

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }


}
