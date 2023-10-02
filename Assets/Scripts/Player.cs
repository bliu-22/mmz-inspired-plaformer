using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    Rigidbody2D rb;
    Animator animator;
    PlayerEffectController playerEffectController;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float gravity;

    [Header("Transform")]
    public PlayerDataStorage playerData;

    [Header("Horizontal Movement")]
    [SerializeField] float runVelocity = 5f;
    [SerializeField] bool isRunning;
    public bool canMove = true;
    // made public for dash slashing check
    [SerializeField] public bool isDashing;

    [SerializeField] float maxDashTime = 1f;
    [SerializeField] float minDashTime = 0.5f;
    [SerializeField] float remainingDashTime;
    [SerializeField] float dashVelocity = 8f;

    [Header("Vertical Movement")]
    [SerializeField] bool canJump = true;
    [SerializeField] float jumpVel = 6f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float hopMultiplier = 2f;

    //public
    [SerializeField] public bool isGrounded;

    [SerializeField] bool canWallSlide;
    [SerializeField] float wallSlideVelocity = 0f;
    // public
    [SerializeField] public bool isWallsliding;

    [SerializeField] Vector2 sameSideWallJump = new Vector2(2, 3);
    [SerializeField] float wallJumpForce;


    [Header("MeleeAttack")]
    // grounded
    [SerializeField] bool duringCombo = false;
    bool groundSlashPressed;
    int comboCount;
    bool isDashSlashing;

    // areial
    bool isAirSlashing;
    bool isRoundSlashing;
    bool isWallSlashing;
    // hitbox
    int slashDamage = 10;
    [SerializeField] Transform groundSlashHitbox, airSlashHitboxA, airSlashHitboxB;
    [SerializeField] Vector2 groundSlashSize, airSlashSizeA, airSlashSizeB;

    // ray
    [SerializeField] Transform groundRayA, groundRayB, wallRay;
    [SerializeField] float groundRayLength, wallRayLength;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerEffectController = GetComponent<PlayerEffectController>();
        sameSideWallJump.Normalize();

        transform.position = playerData.initialPos;
        transform.localScale = new Vector2(playerData.initialFacingDirection, transform.localScale.y);
        //transform.position = RoomSwitch.SpawnPos;
        //transform.localScale = transform.localScale = new Vector2(RoomSwitch.FacingDirection, transform.localScale.y);
        //Debug.Log(transform.position.ToString());
        
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove) 
        {
            HorizontalMovement();
            GroundCombo();
            DashSlash();
            AerialSlash();
            WallSlash();
            WallSliding();
            FlipSprite();
            Jump();

        }
        else 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        // Dash() checks canMove inside the method
        Dash();

        HandleAnimation();
    }

    private void FixedUpdate()
    {
            GroundCheck();
            WallCheck(); 
    }

    private void HandleAnimation() 
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isRunning", isRunning);
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("isDashing", isDashing);
        animator.SetBool("isWallsliding", isWallsliding);
        animator.SetBool("isAirSlashing", isAirSlashing);
        animator.SetBool("isRoundSlashing", isRoundSlashing);
        animator.SetBool("isDashSlashing", isDashSlashing);
        animator.SetBool("isWallSlashing", isWallSlashing);
        animator.SetBool("canMove", canMove);
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.Raycast(groundRayA.position, Vector2.down, groundRayLength, groundLayer) || Physics2D.Raycast(groundRayB.position, Vector2.down, groundRayLength, groundLayer); ;
    }

    private void WallCheck() 
    {
            canWallSlide = rb.velocity.y < 0 && !isGrounded && Physics2D.Raycast(wallRay.position, Vector3.right * transform.localScale.x, wallRayLength, groundLayer);
    }


    private void HorizontalMovement() 
    {

            var horizontalInput = Input.GetAxisRaw("Horizontal");


            if (duringCombo)
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
            }
            else if (!isDashing && !isWallsliding)
            {
                rb.velocity = new Vector2(horizontalInput * runVelocity, rb.velocity.y);
                if ((horizontalInput == -1 || horizontalInput == 1))
                {
                    isRunning = true;
                }
                else { isRunning = false; }

            }
            // for changing direction mid dash
            else if (isDashing && (horizontalInput == -1 || horizontalInput == 1))
            {
                rb.velocity = new Vector2(horizontalInput * dashVelocity, rb.velocity.y);
            }

        


    }
    private void WallSliding()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (canWallSlide && horizontalMovement == transform.localScale.x)
        {
            rb.velocity = new Vector2(rb.velocity.x, wallSlideVelocity);

            isWallsliding = true;
        }
        else
        {
            isWallsliding = false;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // grounded jump
            if (isGrounded && !isDashSlashing)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVel);

            }
            // wall jumping
            if (!isWallSlashing &&(isWallsliding || canWallSlide))
            {
                var horizontalMovement = Input.GetAxisRaw("Horizontal");
                if (horizontalMovement == transform.localScale.x)
                {
                  
                    //add a force pointing up and away from the wall
                    canWallSlide = false;
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(new Vector2(-transform.localScale.x * wallJumpForce * sameSideWallJump.x, wallJumpForce * sameSideWallJump.y), ForceMode2D.Impulse);
                    
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpVel);
                }
            }
        }
        // variable time jump
        if (rb.velocity.y > 0)
        {

            if (!Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (hopMultiplier - 1) * Time.deltaTime;
            }

        }
        else if (rb.velocity.y < 0)
        {
            if (!isWallsliding)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }

        }


    }

    //  
    private void Dash() 
    {
        if (canMove) 
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && (isGrounded || isWallsliding) && !isDashing && !duringCombo)
            {
                isDashing = true;
                rb.velocity = new Vector2(transform.localScale.x * dashVelocity, rb.velocity.y);

                remainingDashTime = maxDashTime;
                // play effects
                playerEffectController.PlayDashEffects();
                playerEffectController.ActivateTrails();
            }
            //dash not held
            if (isDashing && !Input.GetKey(KeyCode.LeftShift))
            {
                //stops dashing if min dash time is reached
                if (maxDashTime - remainingDashTime >= minDashTime)
                {
                    isDashing = false;
                    remainingDashTime = 0;
                    playerEffectController.DisableTrails();
                }
            }
        }
        
        //stops dashing if max dash time is reached even if dash button is held
        //stops dashing if knocked back
        if ((isDashing && remainingDashTime <= 0) || !canMove)
        {
            isDashing = false;
            remainingDashTime = 0;
            playerEffectController.DisableTrails();
        }
        else 
        {
            remainingDashTime -= Time.deltaTime;
        }
    }

    // flip sprite
    private void FlipSprite() 
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        
        
        if (horizontalMovement == 1)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (horizontalMovement == -1)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        
        
    }

    // ATTACK
    // for ground triple slash combo
    private void StartComboWindow()
    {
        groundSlashPressed = false;
        if (comboCount < 3)
        {
            comboCount++;
        }
    }
    private void EndComboWindow()
    {
        groundSlashPressed = false;
        comboCount = 0;
        duringCombo = false;
    }
    private void GroundCombo()
    {
   
        if (isGrounded && !isDashing && Input.GetKeyDown(KeyCode.Z) && !groundSlashPressed)
        {
            groundSlashPressed = true;
            animator.SetTrigger(comboCount.ToString());
            duringCombo = true;
        }
     }
    private void WallSlash() 
    {
        if (isWallsliding & Input.GetKeyDown(KeyCode.Z)) 
        {
            isWallSlashing = true;
        }
    }
    private void DashSlash() 
    {
        if (isDashing && isGrounded && Input.GetKeyDown(KeyCode.Z))
        {
            isDashSlashing = true;
        }

    }
    private void AerialSlash()
    {
        // deal with the case where player landed before slash played to its end
        if (isGrounded) 
        {
            EndAerialAttack();
        }

        if (!isGrounded && !isWallSlashing && Input.GetKeyDown(KeyCode.Z))
        {
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                isRoundSlashing = true;
            } else 
            {
                isAirSlashing = true;
            }   
        }
        if (!isGrounded && Input.GetKeyDown(KeyCode.Z) && Input.GetAxisRaw("Vertical") == -1)
        {
            isRoundSlashing = true;
        }

    }
    private void EndAerialAttack()
    {
        isAirSlashing = false;
        isRoundSlashing = false;
        isWallSlashing = false;
    }
    private void EndDashSlash() 
    {
        isDashSlashing = false;
    }

    // enable corresponding hitbox
    private void EnableHitboxGroundSlash() 
    {
        Collider2D[] objectsHit = Physics2D.OverlapBoxAll(groundSlashHitbox.position, groundSlashSize, 0);
        if (objectsHit != null) 
        {
            foreach (Collider2D obj in objectsHit)
            {
                if (obj.gameObject.CompareTag("Enemy"))
                {
                    obj.SendMessage("TakeDamage", slashDamage);
                }
                if (obj.gameObject.CompareTag("Bullet"))
                {
                    Destroy(obj.gameObject);
                }
            }
        }

    }
    private void EnableHitboxAirSlashA()
    {
        Collider2D[] objectsHit = Physics2D.OverlapBoxAll(airSlashHitboxA.position, airSlashSizeA, 0);

        if (objectsHit != null)
        {
            foreach (Collider2D obj in objectsHit)
            {
                if (obj.gameObject.CompareTag("Enemy"))
                {
                    obj.SendMessage("TakeDamage", slashDamage);
                }
            }
        }
    }
    private void EnableHitboxAirSlashB()
    {
        Collider2D[] objectsHit = Physics2D.OverlapBoxAll(airSlashHitboxB.position, airSlashSizeB, 0);
        if (objectsHit != null)
        {
            foreach (Collider2D obj in objectsHit)
            {
                if (obj.gameObject.CompareTag("Enemy"))
                {
                    obj.SendMessage("TakeDamage", slashDamage);
                }
            }
        }
    }

    
    // for raycast debugging
    private void OnDrawGizmos()
    {
        //visualized ground check ray
        Gizmos.color = Color.white;
        Gizmos.DrawLine(groundRayA.position, groundRayA.position + Vector3.down * groundRayLength);
        Gizmos.DrawLine(groundRayB.position, groundRayB.position + Vector3.down * groundRayLength);
        //visualzied wall check ray
        Gizmos.color = Color.white;
        Gizmos.DrawLine(wallRay.position, wallRay.position + Vector3.right * wallRayLength * transform.localScale.x);

        // visualized hitboxes
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundSlashHitbox.position, groundSlashSize);
        Gizmos.DrawWireCube(airSlashHitboxA.position, airSlashSizeA);
        Gizmos.DrawWireCube(airSlashHitboxB.position, airSlashSizeB);
    }

}
