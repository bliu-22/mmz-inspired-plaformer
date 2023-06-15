using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1StunState : EnemyState
{
    private Enemy1 owner;
    float stunTime;
    float stunCountdown;
    public Enemy1StunState(StateMachine stateMachine, Enemy1 owner, float stunTime) : base(stateMachine)
    {
        this.owner = owner;
        this.stunTime = stunTime;
    }
    public override void Enter()
    {
        Debug.Log("Now In Stun State");
        owner.animator.SetBool("isIdle", true);
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
        owner.animator.SetBool("isIdle", false);
    }

}
