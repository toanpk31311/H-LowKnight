using UnityEngine;


public class StateMachine
{


    public State currentState { get; private set;}

    public void Initialize(State currentState)
    {
        this.currentState = currentState;
        currentState.Enter();
    }

    public void ChangeState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        Debug.Log("Enter new state: " + currentState);
    }

    public void ExitState(State currentState)
    {
        this.currentState = currentState;
        currentState.Exit();
    }


}
