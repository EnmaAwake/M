using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Basics")]
    Rigidbody2D rb;
    BoxCollider2D bc;
    Animator animator;

    [Header("Move")]
    private float moveInput;
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    public float run;
    [SerializeField] private float runMultiplier;


    [Header("FacingComponents")]
    [SerializeField] private float scale;

    [Header("GroundCheck")]
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Inputs

        moveInput = Input.GetAxisRaw("Horizontal");

        //Run Mechanichs
        if(Input.GetKey(KeyCode.Space))
        {
            
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Movement

        float targetSpeed = moveInput * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (MathF.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
        #endregion

        #region Facing < >

        switch (Mathf.Sign(moveInput))
        {
            case >= 1:
                transform.localScale = new Vector2(scale, transform.localScale.y);
                break;
            case <= -1:
                transform.localScale = new Vector2(-scale, transform.localScale.y);
                break;
            default:
                break;
        }
        #endregion

        #region Move Animation
        if(rb.velocity.x != 0)
        {
            animator.SetBool("Moving" , true);
            animator.speed = Mathf.Abs(rb.velocity.x);
        }
        else
        {
            animator.SetBool("Moving" , false);
        }

        #endregion
    }

/*    #region GroundCheck

    private bool isGrounded()
    {
        float extraHeightText = .03f;
        RaycasHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, bc.bounds.extents.y + extraHeightText, groundLayer);
        return raycastHit.collider != null;
    }
    #endregion
*/
}
