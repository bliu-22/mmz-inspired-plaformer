using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1IdleState : EnemyState
{
    private Enemy1 owner;
    private float idleCountdown;
    private float flipDelay;
 
    public Enemy1IdleState(StateMachine stateMachine, Enemy1 owner, float flipDelay) : base(stateMachine)
    {
        this.owner = owner;
        this.flipDelay = flipDelay;
    }
    public override void Enter()
    {
        Debug.Log("Now In Idle State");
        owner.rb.velocity = new Vector2(0, owner.rb.velocity.y);
        owner.animator.SetBool("isIdle", true);
        idleCountdown = flipDelay;
    }
    public override void UpdateState()
    {
        
        idleCountdown -= Time.deltaTime;
        if (idleCountdown <= 0) 
        {
            owner.TurnAround();
            stateMachine.SwitchState(owner.patrolState);
        }
        
    }
    public override void Exit()
    {
        owner.animator.SetBool("isIdle", false);
    }


}
