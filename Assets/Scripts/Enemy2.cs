using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    //
    
    // states
    public Enemy2PatrolState patrolState;
    public Enemy2StunState stunState;
    public Enemy2FlipState flipState;

    public override void Awake() 
    {
        base.Awake();
        patrolState = new Enemy2PatrolState(stateMachine, this);
        stunState = new Enemy2StunState(stateMachine, this, stunTime);
        flipState = new Enemy2FlipState(stateMachine, this, flipDelay);

        stateMachine.SwitchState(patrolState);
    }

    public override void TakeDamage(int damage)
    {
        stateMachine.SwitchState(stunState);
        base.TakeDamage(damage);

    }
}
