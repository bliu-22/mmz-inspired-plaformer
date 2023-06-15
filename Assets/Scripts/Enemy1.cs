using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{

    // player detection
    [SerializeField] Transform playerDetection;
    [SerializeField] Vector2 playerDetectionRange;
    // range attack
    bool isFiring;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed;
    [SerializeField] float firingInterval;
    float firingCountdown;
    [SerializeField] float reloadInterval;
    float reloadCountdown;
    [SerializeField] Transform bulletStartingPoint;
    [SerializeField] float shotCount;

    // states
    public Enemy1PatrolState patrolState;
    public Enemy1IdleState idleState;
    public Enemy1FireState fireState;
    public Enemy1StunState stunState;
     
    public override void Awake()
    {
        base.Awake();

        patrolState = new Enemy1PatrolState(stateMachine, this);
        idleState = new Enemy1IdleState(stateMachine, this, flipDelay);
        fireState = new Enemy1FireState(stateMachine, this, bullet, bulletStartingPoint, bulletSpeed, shotCount, firingInterval, reloadInterval);
        stunState = new Enemy1StunState(stateMachine, this, stunTime);

        stateMachine.SwitchState(patrolState);
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

    // added playerdetection in subclass as some type of enemies dont need it
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        // playerdetection
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(playerDetection.position, playerDetectionRange);
    }
}
