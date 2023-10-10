using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{
    // player detection
    [SerializeField] Transform playerDetection;
    [SerializeField] Vector2 playerDetectionRange;
    [SerializeField] Vector2 playerAttackRange;
    private GameObject player;

    // speed
    [SerializeField] float chaseSpeed;
    [SerializeField] float attackSpeed;

    // position
    [SerializeField] Transform startingPos;


    // states
    public Enemy3PatrolState patrolState;
    public Enemy3ChaseState chaseState;
    public Enemy3AttackState attackState;
    public Enemy3StunState stunState;

    
    public override void Awake()
    {

        base.Awake();
        patrolState = new Enemy3PatrolState(stateMachine, this);
        chaseState = new Enemy3ChaseState(stateMachine, this);
        attackState = new Enemy3AttackState(stateMachine, this);
        stunState = new Enemy3StunState(stateMachine, this, stunTime);
        stateMachine.SwitchState(patrolState);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = startingPos.position;
    }

    public void ResetPosition() 
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPos.position, attackSpeed * Time.deltaTime);
    }

    public override void TakeDamage(int damage)
    {
        stateMachine.SwitchState(stunState);
        base.TakeDamage(damage);

    }

    public bool PlayerCheck()
    {
        // return true when player is detected
        Collider2D[] objectsDetected = Physics2D.OverlapBoxAll(playerDetection.position, playerDetectionRange, 0);
        foreach (Collider2D obj in objectsDetected)
        {
            if (obj.gameObject.CompareTag("Player"))
            {
                return true;
            }

        }
        return false;
    }
    public bool AttackRangeCheck() 
    {
        // return true when player is detected
        Collider2D[] objectsDetected = Physics2D.OverlapBoxAll(playerDetection.position, playerAttackRange, 0);
        foreach (Collider2D obj in objectsDetected)
        {
            if (obj.gameObject.CompareTag("Player"))
            {
                return true;
            }

        }
        return false;
    }

    public void Chase() 
    {
        if (player != null) 
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y + 1f), chaseSpeed * Time.deltaTime);
        }
 
    }
    public void Attack()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y + 1f), attackSpeed * Time.deltaTime);
        }

    }

    public override void OnDrawGizmos()
    {


        // playerdetection
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerDetection.position, playerDetectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(playerDetection.position, playerAttackRange);

    }
}
