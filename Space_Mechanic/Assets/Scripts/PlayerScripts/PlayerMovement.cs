using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpPower;

    private float xDirection = 0;
    private float yDirection = 0;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        xDirection = 0;
        yDirection = 0;
        PlayerInput();

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            yDirection = jumpPower;
        }
    }
}
