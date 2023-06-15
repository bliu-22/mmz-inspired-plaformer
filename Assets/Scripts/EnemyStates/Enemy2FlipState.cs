using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2FlipState : EnemyState
{
    private Enemy2 owner;
    private float flipDelay;
    private float flipCountdown;
    

    public Enemy2FlipState(StateMachine stateMachine, Enemy2 owner, float flipDelay) : base(stateMachine) 
    {
        this.owner = owner;
        this.flipDelay = flipDelay;
    }
    public override void Enter()
    {
        owner.animator.SetTrigger("flip");
        owner.rb.velocity = new Vector2(0, owner.rb.velocity.y);
        flipCountdown = flipDelay;
    }

    public override void UpdateState()
    {
        flipCountdown -= Time.deltaTime;
        if (flipCountdown <= 0)
        {
            stateMachine.SwitchState(owner.patrolState);
        }
    }
    public override void Exit()
    {
        
    }


}
