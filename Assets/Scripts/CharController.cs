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
   
    private Rigidbody2D rb;    
    private float moveInput;
    private bool facingRight = true;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;
    private float jumpCharge, minCharge, maxCharge;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpCharge = 0f;
        minCharge = 2f;
        maxCharge = 15f;

        chargeBar.SetMaxCharge(maxCharge);
        //chargeBar.Hide();
    }

    private void FixedUpdate()
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
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        //{
            Jump();
        //}
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            //chargeBar.Unhide();
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
           if (jumpCharge > 0)
           {
                jumpCharge += minCharge;
                rb.velocity = Vector2.up * jumpCharge;
                jumpCharge = 0f;
            }            
        }

        chargeBar.SetCharge(jumpCharge);
       // chargeBar.Hide();
    }

    bool isGrounded()
    {
       grounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
       return grounded;
    }
}
