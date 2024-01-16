using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
   public PlayerState curentState
    {
       get; private set;    
    }
    public  void Initialize (PlayerState curentState)
    {
        this.curentState = curentState;
        curentState.Enter();
    }
    public void ChangeState (PlayerState newState) {
        curentState.Exit();
        curentState = newState; 
        curentState.Enter ();

    }
}
