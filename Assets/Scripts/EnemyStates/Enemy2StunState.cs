using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2StunState : EnemyState
{
    private Enemy2 owner;
    private float stunTime;
    private float stunCountdown;
    public Enemy2StunState(StateMachine stateMachine, Enemy2 owner, float stunTime) : base(stateMachine) 
    {
        this.owner = owner;
        this.stunTime = stunTime;
    }
    public override void Enter()
    {
        stunCountdown = stunTime;
        owner.rb.velocity = new Vector2(0, owner.rb.velocity.y);
    }
    public override void UpdateState()
    {
        stunCountdown -= Time.deltaTime;
        if (stunCountdown <= 0)
        {
            stateMachine.SwitchState(owner.patrolState);
        }
    }
    public override void Exit()
    {

    }


}
