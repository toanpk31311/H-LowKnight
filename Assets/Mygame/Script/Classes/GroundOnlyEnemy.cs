using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundOnlyEnemy : Entity
{   
    [SerializeField]protected LayerMask WhatIsPlayer;
    [Header("Move info")]
    public float moveSpeed = 1.5f;
    public float idleTime = 2;
    public float BatleStateTimer = 1.2f;
    public float battleTime = 7;
    private float defaultMoveSpeed;


    [Header("Atck info")]
    public float attackDistance = 2;
    public float agroDistance = 2;
    public float attackCooldown;
    public float minAttackCooldown = 1;
    public float maxAttackCooldown = 2;
    [HideInInspector] public float lastTimeAttacked;
    
    [Header("Stunned info")]
    public float stunDuration = 1;
    public Vector2 stunDirection = new Vector2(10, 12);
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;


    public EnemyStateMachine stateMachine { get; private set; }
    private Player player;
    public string lastAnimBoolName { get; private set; }
    protected override  void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultMoveSpeed = moveSpeed;
        
    }
    protected override void Update()
    {
        base.Update(); 
        stateMachine.currentState.Update();
    }
    #region Counter Attack Window
    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    #endregion

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDr, 50, WhatIsPlayer);//
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDr, transform.position.y));
    }
    public virtual void FreezeTime(bool _timeFrozen)
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }
    public virtual void FreezeTimeFor(float _duration) => StartCoroutine(FreezeTimerCoroutine(_duration));

    protected virtual IEnumerator FreezeTimerCoroutine(float _seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTime(false);
    }

}
