using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3AttackState : EnemyState
{
    // Start is called before the first frame update
    private Enemy3 owner;
    public Enemy3AttackState(StateMachine stateMachine, Enemy3 owner) : base(stateMachine)
    {
        this.owner = owner;
    }
    public override void Enter()
    {
        owner.animator.SetBool("isAttacking", true);
        owner.Attack();
    }

    public override void Exit()
    {
        owner.animator.SetBool("isAttacking", false);
    }

    public override void UpdateState()
    {
        owner.Attack();
        if (!owner.AttackRangeCheck())
        {
            stateMachine.SwitchState(owner.chaseState);
        }
        if (!owner.PlayerCheck()) 
        {
            stateMachine.SwitchState(owner.patrolState);
        }
    }

}
