using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1PatrolState : EnemyState
{
    private Enemy1 owner;
    //public EnemyPatrolState(Enemy owner, StateMachine stateMachine, string animatorBool):base(owner, stateMachine, animatorBool)
    public Enemy1PatrolState(StateMachine stateMachine, Enemy1 owner) : base(stateMachine)
    {
        this.owner = owner;
    }
    public override void Enter()
    {
        Debug.Log("Now In Patrol State");
        //base.Enter();
        owner.rb.velocity = new Vector2(owner.walkSpeed * owner.transform.localScale.x, owner.rb.velocity.y);
        owner.animator.SetBool("isRunning", true);
    }
    public override void UpdateState()
    {
        //base.Update();
        owner.rb.velocity = new Vector2(owner.walkSpeed * owner.transform.localScale.x, owner.rb.velocity.y);
        owner.EnvironmentCheck();
        if (owner.isGrounded &(owner.wallAhead || owner.edgeAhead)) 
        {
            Debug.Log("turn");
            stateMachine.SwitchState(owner.idleState);
        }
           
        if (owner.PlayerCheck()) 
        {
            stateMachine.SwitchState(owner.fireState);
        }
    }
    public override void Exit()
    {
        //base.Exit();
        owner.animator.SetBool("isRunning", false);
    }

}
