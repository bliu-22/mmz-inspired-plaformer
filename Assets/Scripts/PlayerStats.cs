using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    Animator animator;
    Player playercontroller;
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float curHealth;
    [SerializeField] bool isInvul;
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerDataStorage playerData;
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = GetComponent<Player>();
        animator = GetComponent<Animator>();
        curHealth = playerData.initialHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //for testing
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            Debug.Log(":");
            TakeDamage(5);
        }
      
    }

    public float GetHealth() 
    {
        return curHealth;
        //Debug.Log(curHealth);
    }
    public void SetHealth(float health) 
    {
        curHealth = health;
    }


    private void TakeDamage(int damage)
    {
        if (!isInvul) 
        {
            curHealth -= damage;
            animator.SetTrigger("knockedBack");
            playercontroller.canMove = false;
            isInvul = true;
            Invoke("EnableMovement", 0.8f);
            Invoke("EndInvul", 1.5f);
        }
        if (curHealth <= 0)
        {
            gameManager.triggerGameOver();
            GameObject deathEffectInstance = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }

    }
    private void EndInvul()
    {
        isInvul = false;
    }
    private void EnableMovement() 
    {
        
        playercontroller.canMove = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Killzone"))
        {
            TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
        }

    }
    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }

}
