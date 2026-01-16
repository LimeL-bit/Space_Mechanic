using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;
    [SerializeField] float climbSpeed;
    [SerializeField] float gravity;

    private bool canJump = true;
    private bool isJumping = false;
    private bool canClimbLadder = false;
    private bool isClimbing = false;
    public AudioSource jumpSound;

    private float xDirection = 0;
    private float yDirection;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        rb.gravityScale = gravity;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if your climbing
        if (!isClimbing)
        {
            rb.gravityScale = gravity;
        }
        xDirection = 0;
        yDirection = rb.linearVelocity.y;
        
        PlayerInput();
        Climb();

        animator.SetFloat("Movement", Mathf.Abs(xDirection));
        animator.SetBool("IsJumping", isJumping);
        rb.linearVelocity = new Vector2(xDirection, yDirection);
    }

    private void PlayerInput()
    {
        //move right
        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            xDirection = speed;
        }

        //move left
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            xDirection = -speed;
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isClimbing)
        {
            isJumping = true;
            animator.SetTrigger("Jump");
            yDirection = jumpPower;
            jumpSound.Play();
            canJump = false;
        }
    }
    private void Climb()
    {
        //the code wont work if canClimb is true
        if (!canClimbLadder)
        {
            return;
        }

        if (isClimbing)
        {
            rb.gravityScale = 0;
        }

        //climb up
        if (Input.GetKey(KeyCode.W))
        {
            yDirection = climbSpeed;
            isClimbing = true;
            animator.SetBool("IsClimbing", isClimbing);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            yDirection = 0;
        }
        

        //climb down
        if (Input.GetKey(KeyCode.S))
        {
            yDirection = -climbSpeed;
            isClimbing = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            yDirection = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            canJump = true;
            isJumping = false;
            animator.SetTrigger("Land");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            canClimbLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            canClimbLadder = false;
            isClimbing = false;
            animator.SetBool("IsClimbing", isClimbing);
        }
    }
}
