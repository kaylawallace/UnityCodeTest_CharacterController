using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharController : MonoBehaviour
{
    public float speed, jumpForce;
    public float checkRadius;
    public Transform feetPos;
   
    private Rigidbody2D rb;    
    private float moveInput;
    private bool facingRight = true;
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
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
        rb.velocity = Vector2.up * jumpForce;
    }

    bool isGrounded()
    {
       grounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
       return grounded;
    }
}
