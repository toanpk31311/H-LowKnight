using UnityEngine;

public class GroundEnemy : Entities
{


    public StateMachine stateMachine { get; private set; }


    protected override  void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine();
    }

    protected override void Update()
    {
        base.Update(); 
        stateMachine.currentState.Update();
    }


}
