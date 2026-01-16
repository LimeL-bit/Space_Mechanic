using UnityEngine;

public class SpaceBackground : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    [SerializeField] float speed = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(-speed, 0);

        if(transform.position.x < -73)
        {
            transform.position = Vector2.zero;
        }
    }
}
