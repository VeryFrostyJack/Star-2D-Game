using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;
    public int maxJumpCount;

    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private bool isGrounded;
    private int jumpCount;

    // Awake is called after all objects are initialized. Called in a random order.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Start()
    {
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs
        ProcessInputs();

        // Animate
        ProcessAnimation();
    }
    
    // better for physics, can be called a bunch of times per update frame
    private void FixedUpdate()
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if (isGrounded && !isJumping)
        {
            jumpCount = maxJumpCount;
        }
        // Move 
        ProcessMovement();
    }
    private void ProcessMovement()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
        if (isJumping)
        {
            jumpCount--;
            rb.AddForce(new Vector2(0f, jumpForce));
            
        }
        isJumping = false;
    }

    private void ProcessAnimation()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal"); // scale of -1 to 1
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
