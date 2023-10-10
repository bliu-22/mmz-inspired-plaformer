using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3PatrolState : EnemyState
{
    private Enemy3 owner;

    public Enemy3PatrolState(StateMachine stateMachine, Enemy3 owner) : base(stateMachine)
    {
        this.owner = owner;
    }
    public override void Enter()
    {
        
        owner.animator.SetBool("isFlying", true);
    }

    public override void Exit()
    {
        owner.animator.SetBool("isFlying", false);
    }

    public override void UpdateState()
    {
        if (owner.PlayerCheck())
        {
            stateMachine.SwitchState(owner.chaseState);
        }
        else {

            owner.ResetPosition();
        }
    }


}
