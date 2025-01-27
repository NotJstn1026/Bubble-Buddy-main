using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField] private Collider2D playerCollider2D;

    [SerializeField] private Animator animator;

    private float horizontalValue;

    private float radius = 0.1f;

    //Speed Multiplicator
    [SerializeField] private readonly float walkSpeed = 400f;
    [SerializeField] private readonly float airSpeed = 300f;

    //Jump Multiplicator
    [SerializeField] private readonly float groundJump = 8f;

    //Current States of the Player
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isFacingRight = true;

    private Vector2 velocity;

    //LayerMask
    [SerializeField] LayerMask collideLayer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        velocity = rb.velocity;
        horizontalValue = Input.GetAxisRaw("Horizontal");
        Walk();
        Jump();
        GroundCheck();

        if (velocity.y < -3f) velocity.y = -3f;

        rb.velocity = velocity;
    }

    private void Jump()
    {
        //Input for Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //rb.AddForce(new Vector2(rb.velocity.x, groundJump), ForceMode2D.Impulse);

             velocity.y = groundJump;
             
        }

    }

    private void Walk()
    {

        float calculatedSpeed = isGrounded ? walkSpeed : airSpeed;
        float speed = horizontalValue * calculatedSpeed * Time.deltaTime;
        Debug.Log(speed);

        //Add the new Velocity with a Vector 2
        //rb.velocity = new Vector2(speed, rb.velocity.y);
        velocity.x = speed;

        //If looking right and clicked left (flip to the left)
        Vector3 animatorScale = animator.transform.localScale;
        if (speed < 0 && isFacingRight)
        {
            animatorScale.x *= -1;
            animator.transform.localScale = animatorScale;
            isFacingRight = false;
        }

        //If looking right and clicked right (flip to the right) 
        else if (!isFacingRight && speed > 0)
        {
            animatorScale.x *= -1;
            animator.transform.localScale = animatorScale;
            isFacingRight = true;
        }

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", Mathf.Abs(rb.velocity.y));
    }

    /// <summary>
    /// It Checks if the Player is colliding the ground and set the isGrounded Variable.
    /// </summary>
    private void GroundCheck()
    {
        //Bounds playerBounds = playerCollider2D.bounds;

        //float offSetY = transform.position.y - playerBounds.extents.y + playerCollider2D.offset.y;
        //Vector2 origin = new Vector2(transform.position.x + playerCollider2D.offset.x, offSetY);

        Vector2 origin2 = new Vector2(transform.position.x, transform.position.y - 0.5f);

        isGrounded = Physics2D.OverlapCircle(origin2, radius, collideLayer);
    }
}
