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
    private float playerAngle;
    public float jumpExplodePower = 32f;
    private float jAngle;
    public ReticleBehavior playerReticleBehavior;

    //SerializeField
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
  

    // Update is called once per frame
    void Update()//use inputs to call functions
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
        JumpExplode();
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed,rb.velocity.y);
    }

    private void JumpExplode ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //Working from Unity Lesson: https://learn.unity.com/tutorial/scope-and-access-modifiers?uv=2019.3&projectId=5c8920b4edbc2a113b6bc26a#5c8a40e9edbc2a001f47ccef
            //set the value of angle equal to the angle found from the reticle
            jAngle = playerReticleBehavior.ReticleAngle();
            //Debug.Log("current reticle angle (in degrees) is: "+jAngle);
            jAngle *= Mathf.Deg2Rad;
            //Debug.Log("current reticle angle (in radians) is: "+jAngle);
            
            float hjump = jumpExplodePower*Mathf.Cos(jAngle);
            float vjump = jumpExplodePower*Mathf.Sin(jAngle);

            Debug.Log("horizontal force applied is: "+hjump);
            //Debug.Log("vertical force applied is: "+vjump);
            
            //rb.velocity = new Vector2(hjump, vjump);

            //rb.AddForce(transform.up*vjump, ForceMode2D.Impulse);
            rb.AddForce(transform.forward*hjump,ForceMode2D.Impulse);
        } 
    }
    
}
