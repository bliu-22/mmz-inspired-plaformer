using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    //protected Enemy owner;
    protected StateMachine stateMachine;
    //protected string animatorBool;
    public EnemyState(StateMachine stateMachine)
    //public EnemyState(Enemy owner, StateMachine stateMachine, string animatorBool)
    {
        //this.name = name;
        this.stateMachine = stateMachine;
        //this.animatorBool = animatorBool;
        //this.owner = owner;
    }
    //base methods to be overridden by subclasses
    public abstract void Enter();
    public abstract void UpdateState();
    //public abstract void FixedUpdate() { }
    public abstract void Exit();
}
