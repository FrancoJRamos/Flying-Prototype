using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //working from video: https://www.youtube.com/watch?v=K1xZ-rycYY8
    //declare variables
    private float horizontal;
    private float speed =8f;
    private float jumpingPower =16f;
    public bool isFacingRight = true;

    //SerializeField
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");//returns a value of -1, 0, or 1

        if (Input.GetButtonDown("Jump") && IsGrounded())//if jump button is held, get the full jump power
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);    
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)//if jump button is tapped, get a reduced jump power
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
        }
        //Flip();
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed,rb.velocity.y);
    }


    /*
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    */
}
