using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1FireState : EnemyState
{
    private Enemy1 owner;
    Transform bulletStartingPoint;
    GameObject bullet;
    float bulletSpeed, shotCount, firingInterval, reloadInterval, reloadCountdown, firingCountdown;
    public Enemy1FireState(StateMachine stateMachine, Enemy1 owner, GameObject bullet, Transform bulletStartingPoint, float bulletSpeed, float shotCount, float firingInterval, float reloadInterval) : base(stateMachine)
    {
        this.owner = owner;
        this.bulletStartingPoint = bulletStartingPoint;
        this.bullet = bullet;
        this.bulletSpeed = bulletSpeed;
        this.shotCount = shotCount;
        this.firingInterval = firingInterval;
        this.reloadInterval = reloadInterval;
    }
    public override void Enter()
    {
        Debug.Log("Now In Fire State");
        owner.animator.SetBool("isFiring", true);
        reloadInterval = 3f;
        shotCount = 3;
        reloadCountdown = 0f;
        firingCountdown = firingInterval;
    }

    public override void UpdateState()
    {
        if (shotCount <= 0)
        {
            reloadCountdown -= Time.deltaTime;
            if (reloadCountdown <= 0f)
            {
                shotCount = 3;
            }
        }
        else
        {
            reloadCountdown = reloadInterval;
            firingCountdown -= Time.deltaTime;
            if (firingCountdown <= 0f)
            {
                Fire();
                firingCountdown = firingInterval;
            }

        }

        if (!owner.PlayerCheck()) 
        {
            stateMachine.SwitchState(owner.patrolState);
        }
    }
    public override void Exit()
    {
        owner.animator.SetBool("isFiring", false);
    }

    void Fire()
    {

            GameObject enemyBullet = UnityEngine.Object.Instantiate(bullet, bulletStartingPoint.position, Quaternion.identity);
            enemyBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * owner.transform.localScale.x, 0);
            shotCount -= 1;
            //yield return new WaitForSeconds(firingInterval);
        
    }

}

