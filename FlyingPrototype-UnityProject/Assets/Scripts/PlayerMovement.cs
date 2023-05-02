using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //working from video: https://www.youtube.com/watch?v=K1xZ-rycYY8
    //declare variables
    private float horizontal;
    private float speed =10f;
    private float jumpingPower =60f;
    private float jumpExplodePower = 80f;
    private float jAngle;
    public ReticleBehavior playerReticleBehavior;
    private bool isExploding = false;
    private Animator animator;

    //SerializeField
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
  
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()//use inputs to call functions
    {
        horizontal = Input.GetAxisRaw("Horizontal");//returns a value of -1, 0, or 1

        if (Input.GetButtonDown("Jump") && IsGrounded())//if jump button is held, get the full jump power
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);    
            animator.SetTrigger("Jump");
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)//if jump button is tapped, get a reduced jump power
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
            
        }
        JumpExplode();

        //Debug.Log(horizontal);
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
    }

    private void FixedUpdate()
    {
        if(isExploding)
        {
            //Working from Unity Lesson: https://learn.unity.com/tutorial/scope-and-access-modifiers?uv=2019.3&projectId=5c8920b4edbc2a113b6bc26a#5c8a40e9edbc2a001f47ccef
            
            float hjump = jumpExplodePower*Mathf.Cos(jAngle)*-1*5;
            float vjump = jumpExplodePower*Mathf.Sin(jAngle) * -1;

            //Debug.Log("horizontal force applied is: "+hjump);
            //Debug.Log("vertical force applied is: "+vjump);
            
            //rb.velocity = new Vector2(hjump, vjump);

            rb.AddForce(transform.up*vjump, ForceMode2D.Impulse);
            rb.AddForce(transform.right*hjump,ForceMode2D.Impulse);

            isExploding = false;
        }else
        {
            if(horizontal == -1){
                rb.velocity = new Vector2(horizontal * speed*3/4,rb.velocity.y);
                //rb.AddForce(transform.right*speed*3/4*-1,ForceMode2D.Force);
                animator.SetBool("Sneak",true);
            }
            else if(horizontal == 1){
                rb.velocity = new Vector2(horizontal * speed,rb.velocity.y);


                //rb.AddForce(transform.right*speed,ForceMode2D.Force);
                //Animation, working from youtube video: https://www.youtube.com/watch?v=1Ll1fy2EehU&list=PLKUARkaoYQT178f_Y3wcSIFiViW8vixL4&index=7
                animator.SetBool("Run", true);
            }else{
                animator.SetBool("Run",false);
                animator.SetBool("Sneak",false);
            }
        }
    }

    private void JumpExplode ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            //set the value of angle equal to the angle found from the reticle
            jAngle = playerReticleBehavior.ReticleAngle();
            //Debug.Log("current reticle angle (in degrees) is: "+jAngle);
            jAngle *= Mathf.Deg2Rad;
            Debug.Log("current reticle angle (in radians) is: "+jAngle);

            //set a bool to calculate vectors inside FixedUpdate()
            isExploding = true;

            //play animation based on angle value in radians
            if (jAngle > Mathf.PI/-4 && jAngle < Mathf.PI/4){
                animator.SetTrigger("Explode-Backward");
            }else if (jAngle > Mathf.PI/4 && jAngle < 3*Mathf.PI/4){
                animator.SetTrigger("Explode-Down");
            }else if (jAngle > -3*Mathf.PI/4 && jAngle < Mathf.PI/-4){
                animator.SetTrigger("Explode-Up");
            }else{
                animator.SetTrigger("Explode-Forward");
            }
        } 
    }
    
}
