using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public EnemyState curState;



    // Update is called once per frame
    public void Update()
    {
        curState.UpdateState();
    }

    public void SwitchState(EnemyState newState)
    {
        if (curState != null) 
        {
            curState.Exit();
        }
        curState = newState;
        curState.Enter();
    }

}
