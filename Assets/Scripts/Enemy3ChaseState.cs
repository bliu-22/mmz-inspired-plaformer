using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3ChaseState : EnemyState
{
    private Enemy3 owner;
    public Enemy3ChaseState(StateMachine stateMachine, Enemy3 owner) : base(stateMachine)
    {
        this.owner = owner;
    }
    public override void Enter()
    {
        owner.Chase();
        owner.animator.SetBool("isFlying", true);
    }

    public override void Exit()
    {
        owner.animator.SetBool("isFlying", false);
    }

    public override void UpdateState()
    {
        owner.Chase();
        if (owner.AttackRangeCheck()) 
        {
            stateMachine.SwitchState(owner.attackState);
        }
        if (!owner.PlayerCheck())
        {
            stateMachine.SwitchState(owner.patrolState);
        }
    }


}
