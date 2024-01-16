using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public bool isBusy { get; private set; }
    [Header("Attack infor")]
    public float[] attackMovement;

    [Header("Move infor")]
    
    public float moveSpeed = 2f;
    public float jumpForce = 120f;
    public float facingDr { get; private set; } = 1;
    private bool rightFlip = true;
    [Header("Dash Infor")]
    [SerializeField] private float dashCollDown;
    private float usedashTimer;
    public float dashSpeed;
    public float dashDurarion;
    public float dashDir {  get; private set; }  


    [Header("colision infor")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatISGround;
    [SerializeField] private LayerMask whatIsWall;
    #region Component
    public Animator anim;
    public Rigidbody2D rb;
    #endregion
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
    #endregion
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        PlayerIdleState = new PlayerIdleState(this,StateMachine,"Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        PlayerJumpState= new PlayerJumpState(this, StateMachine, "jump");
        PlayerAirState = new PlayerAirState(this, StateMachine, "jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        slideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        wallJump = new PlayerWallJump(this, StateMachine, "jump");
        primaryAtck = new PlayerPrimaryAtckState(this, StateMachine, "Atack");
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(PlayerIdleState);
        anim= GetComponentInChildren<Animator>();
    }
    private void Update()
    {
       
        StateMachine.curentState.Update();
        CheckForDashInput();
    }
    public void SetVelocity(float _xVelocity,float _yVelocity)
    {
        rb.velocity= new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down,groundCheckDistance, whatISGround);
    public bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right*facingDr, wallCheckDistance, whatISGround);


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+ wallCheckDistance, wallCheck.position.y ));
    }
    public void Flip()
    {
        facingDr = facingDr * -1;
        rightFlip = !rightFlip;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if (_x > 0 && ! rightFlip)
        {
            Flip();
        } else if(_x < 0 && rightFlip)
        {
            Flip();
        }
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
