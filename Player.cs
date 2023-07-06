using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Basics")]
    Rigidbody2D rb;
    BoxCollider2D bc;
    Animator animator;

    [Header("Instances")]
    public static Player pInstance;

    [Header("Move")]
    private float moveInput;
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    [SerializeField] private bool runRequest;

    [Header("Jump")]
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    [SerializeField] private bool jumpRequest;

    [Header("LayerChecks")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;


    [Header("Facing")]
    private bool facingRight;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        if(pInstance == null)
        {
            pInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Inputs
        moveInput = Input.GetAxisRaw("Horizontal");
        
        if(Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpRequest = true;
        }
        else if(Input.GetButtonDown("Jump") && onTheWall())
        {
            rb.velocity = new Vector2(rb.velocity.x , jumpForce);
        }
        else if(Input.GetButton("Jump") && !isGrounded())
        {
            animator.SetBool("Jump",true);
        }
        #endregion


    }

    void FixedUpdate()
    {
        #region Movement

        horizontalMove();

        #endregion
        
        #region Jump
        if(rb.velocity.y < 0) 
        {
            rb.gravityScale = fallMultiplier;
        } 
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump")) 
        {
            rb.gravityScale = lowJumpMultiplier;
        } 
        else 
        {
            rb.gravityScale = 1f;
        }

        if(jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        #endregion

        #region Facing < >
        if(moveInput < 0 && !facingRight)
        {
            flip();
        }
        else if(moveInput > 0 && facingRight)
        {
            flip();
        }
        #endregion

        #region Animations
        animator.speed = 0.25f + Mathf.Abs(rb.velocity.x);        
        if(rb.velocity.x != 0)
        {
            animator.SetBool("Moving" , true);
        }
        else
        {
            animator.SetBool("Moving" , false);
        }

        if(isGrounded())
        {
            animator.SetBool("Jump", false);
        }
        #endregion
    }

    #region GroundCheck

    private bool isGrounded()
    {
        float extraHeightText = 10E-3f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, extraHeightText, groundLayer);
        return raycastHit.collider != null;
    }
    #endregion

    private void horizontalMove()
    {
        float targetSpeed = moveInput * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (MathF.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        rb.AddForce(movement * Vector2.right);
    }

    private void flip() 
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private bool onTheWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
    }
}
