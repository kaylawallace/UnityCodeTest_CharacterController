using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharController : MonoBehaviour
{
    public float speed, jumpForce;
    public float checkRadius;
    public Transform feetPos;
    public JumpChargeBar chargeBar;
    public Animator anim;
   
    private Rigidbody2D rb;
    private float moveInput;
    private bool facingRight; 
    private bool grounded, jumping, landing;
    private float jumpCharge, minCharge, maxCharge;

    [SerializeField] private LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim.SetInteger("state", 0);
        jumpCharge = 0f;
        minCharge = 5f;
        maxCharge = 15f;
        facingRight = true;
        jumping = false;
        landing = false;
        chargeBar.SetMaxCharge(maxCharge);
    }

    private void FixedUpdate()
    {
        if (!jumping)
        {
            Run();
        }
        // stops player from being able to move in 'charging' state 
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Update()
    {
        Jump();
    }

    // Method to flip player depending on which direction they are moving in 
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Run()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);       

        if (!facingRight && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput < 0)
        {
            Flip();
        }

        if (moveInput != 0)
        {
            // Set animation to 'running' 
            anim.SetInteger("state", 1);
        }
        else
        {
            // Set animation to 'idle' 
            anim.SetInteger("state", 0);
        }      
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            jumping = true;
            // Set animation to 'jumpStart'
            anim.SetInteger("state", 2);
            
            // Increment jump charge until jump charge = maximum charge & apply value to the UI bar
            if (jumpCharge < maxCharge)
            {
                jumpCharge += Time.deltaTime*15f;
            }
            else
            {
                jumpCharge = maxCharge;
            }
            chargeBar.SetCharge(jumpCharge);
        }
        else
        {
            // Run when user lets go of jump button
            if (jumpCharge > 0)
            {
                jumping = true;
                jumpCharge += minCharge;    // Add a minimum charge so that the player always jumps at least a little 
                rb.velocity = Vector2.up * jumpCharge;
                landing = true;
                jumpCharge = 0f;
                jumping = false;
            }

            if (rb.velocity.y > 0.2)
            {
                // Set animation to jump
                anim.SetInteger("state", 3);
            }
            else if (rb.velocity.y < 0.1f && landing)
            {
                // Set animation to fall
                anim.SetInteger("state", 4);
                
                if (isGrounded() && landing)
                {
                    // Set animation to land
                    anim.SetInteger("state", 5);
                    landing = false;
                }
            }
        }
        // Reset jump charge UI bar
        chargeBar.SetCharge(jumpCharge);
    }

    /*
     * Method to determine whether player is on the ground 
     * Return: bool grounded - true if player is on the ground 
     */
    bool isGrounded()
    {
       grounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
       return grounded;
    }
}
