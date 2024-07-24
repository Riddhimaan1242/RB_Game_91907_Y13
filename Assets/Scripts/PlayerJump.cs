using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerJump : MonoBehaviour
{
   [Header("Jump Details")]
   public float jumpForce;
   public float jumpTime;
   private float jumpTimeCounter;
   private bool stoppedJumping;

   [Header("Ground Details")]
   [SerializeField] private Transform groundcheck;
   [SerializeField] private float radOCircle;
   [SerializeField] private LayerMask whatIsGround;
   public bool grounded;

   [Header("Components")]
   private Rigidbody2D rb;
   private Animator myAnimator;

   private void Start()
   {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
   }

   private void Update()
   {
        //To know whether the user is on the ground or not
        grounded = Physics2D.OverlapCircle(groundcheck.position,radOCircle,whatIsGround);

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("falling", false);
        }

        //using space to jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stoppedJumping = false;
            //telling animator to play jump up animation
            myAnimator.SetTrigger("jump");
        }

        //To keep accelerating up shortly while jump button is held: for variable jump height  
        if (Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
            //telling animator to play jump up animation
            myAnimator.SetTrigger("jump");
        }

        if (Input.GetButtonUp("Jump"))
        {
          jumpTimeCounter = 0;
          stoppedJumping = true;
          //telling animator to falling animation
          myAnimator.SetBool("falling", true);
          myAnimator.ResetTrigger("jump");
        }

        if (rb.velocity.y < 0)
        {
            myAnimator.SetBool("falling", true);
        }
   }

   private void OnDrawGizmos()
   {
        Gizmos.DrawSphere(groundcheck.position, radOCircle);
   }

   private void FixedUpdate()
   {
        HandleLayers();
   }

   private void HandleLayers()
   {
        if (!grounded)
        {
            myAnimator.SetLayerWeight(1,1);
        }
        
        else
        {
            myAnimator.SetLayerWeight(1,0);
        }
   }
}
