using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public StateMachine stateMachine;
    public Material whiteMat, defaultMat;

    [SerializeField] GameObject meleeParticle;
    [SerializeField] GameObject explosionEffect;
    //basic stats
    [SerializeField] private int enemyHealth = 30;
    [SerializeField] private int curHealth;

    // movement
    [SerializeField] public float walkSpeed;
    [SerializeField] protected float flipDelay;
    
    //environment detection
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform wallRay, edgeRay, groundRay;
    [SerializeField] float wallRayLength, edgeRayLength, groundRayLength;
    [SerializeField] public bool isGrounded, wallAhead, edgeAhead;

    // on hit
    [SerializeField] protected float stunTime;
    [SerializeField] float flashTimeOnHit;
    
    public virtual void Awake()
    {
        
        stateMachine = new StateMachine();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
        whiteMat = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        defaultMat = sr.material;

        curHealth = enemyHealth;

        //patrolState = new EnemyPatrolState(this, stateMachine);
        //idleState = new EnemyIdleState(this, stateMachine);
        //fireState = new EnemyFireState(this, stateMachine, bullet, bulletStartingPoint, bulletSpeed, shotCount, firingInterval, reloadInterval);
        //stunState = new EnemyStunState(this, stateMachine, stunTme);
    }
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        
        stateMachine.Update();
    }


    public void EnvironmentCheck()
    {
        isGrounded = Physics2D.Raycast(groundRay.position, Vector2.down, groundRayLength, groundLayer);
        wallAhead = Physics2D.Raycast(wallRay.position, Vector2.right * transform.localScale.x, wallRayLength, groundLayer);
        // if there is no ground ahead
        edgeAhead = isGrounded && !Physics2D.Raycast(edgeRay.position, Vector2.down, edgeRayLength, groundLayer);
    }

    public virtual void TakeDamage(int damage)
    {
        
        curHealth -= damage;
        
        GameObject particle = Instantiate(meleeParticle, transform.position, Quaternion.identity);
        //StartCoroutine(FlashOnHit());
        FlashOnHit();
        Invoke("ResetFlash", flashTimeOnHit);
    
        if (curHealth <= 0)
        {
            //animator.SetTrigger("death");
            GameObject explosionEffectInstance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            DestroyEnemy();

        }
    }

    void FlashOnHit() 
    {
            sr.material = whiteMat;
    }
    void ResetFlash() 
    {
        sr.material = defaultMat;
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    public void TurnAround()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
    // visualised ray
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(wallRay.position, wallRay.position + Vector3.right * wallRayLength * transform.localScale.x);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(edgeRay.position, edgeRay.position + Vector3.down * edgeRayLength);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundRay.position, groundRay.position + Vector3.down * groundRayLength);

    }
}
