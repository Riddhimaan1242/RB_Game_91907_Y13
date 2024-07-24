using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // Required to state in order to have working animations and physics
    private Rigidbody2D rb;
    private Animator myAnimator;

    private bool facingRight = true;
    private float dirX;

    // varaibles to determine physics value of player
    [SerializeField] private float runSpeed = 2.0f;

    public float horizMovement; // 1[OR]-1[OR]0

    void Start()
    {
        // define gameObject found on the player
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // check direction given by player
        horizMovement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        // move character left and right
        rb.velocity = new Vector2(horizMovement*runSpeed, rb.velocity.y);
        Flip(horizMovement);
        myAnimator.SetFloat("RunSpeed", Mathf.Abs(horizMovement));
    }

    // Method to flip character to face direcion of movement
    private void Flip (float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight; 

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
