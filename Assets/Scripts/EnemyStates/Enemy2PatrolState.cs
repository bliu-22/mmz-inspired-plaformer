using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2PatrolState : EnemyState
{
    private Enemy2 owner;
    public Enemy2PatrolState(StateMachine stateMachine, Enemy2 owner) : base(stateMachine) 
    {
        this.owner = owner;
    }
    public override void Enter()
    {
        owner.rb.velocity = new Vector2(owner.walkSpeed * owner.transform.localScale.x, owner.rb.velocity.y);
        owner.animator.SetBool("isRunning", true);
    }
    public override void UpdateState()
    {
        owner.rb.velocity = new Vector2(owner.walkSpeed * owner.transform.localScale.x, owner.rb.velocity.y);
        owner.EnvironmentCheck();
        if (owner.isGrounded & (owner.wallAhead || owner.edgeAhead))
        {
            //Debug.Log("turn");
            stateMachine.SwitchState(owner.flipState);
        }
    }
    public override void Exit()
    {
        owner.animator.SetBool("isRunning", false);
    }

}
