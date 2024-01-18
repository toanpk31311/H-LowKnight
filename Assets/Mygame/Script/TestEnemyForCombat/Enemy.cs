using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
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



    public EnemyStateMachine stateMachine { get; private set; }
    private PlayerControll player;
    public string lastAnimBoolName { get; private set; }
    protected override  void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        
    }
    protected override void Update()
    {
        base.Update(); 
        stateMachine.currentState.Update();
    }
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDr, 50, WhatIsPlayer);//
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDr, transform.position.y));
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
