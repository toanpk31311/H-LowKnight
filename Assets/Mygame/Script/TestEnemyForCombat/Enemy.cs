using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Move info")]
    public float moveSpeed = 1.5f;
    public float idleTime = 2;
    public float battleTime = 7;
    private float defaultMoveSpeed;
    public EnemyStateMachine stateMachine { get; private set; }
    private PlayerControll player;
    public string lastAnimBoolName { get; private set; }
    protected override  void Awake()
    {
        //base.Awake();
        stateMachine = new EnemyStateMachine();

        
    }
    protected override void Update()
    {
        base.Update(); 
        stateMachine.currentState.Update();
    }
}
