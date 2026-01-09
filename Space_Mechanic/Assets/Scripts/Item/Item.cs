using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    protected int itemCount = 1;

    [SerializeField] float flySpeed = 5;
    private bool canFlyToPlayer = false;
    private GameObject player;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
    private void Update()
    {
        FlyToPlayer();

        if (!canFlyToPlayer)
        {
            return;
        }

        if(Vector2.Distance(transform.position, player.transform.position) < 0.5f)
        {
            PickUp();
        }
    }
    public Item(int itemCount)
    {
        this.itemCount = itemCount;
    }
    public Item()
    {
        itemCount = 1;
    }

    public void FlyToPlayer()
    {
        if (canFlyToPlayer)
        {
            rb.linearVelocity = (player.transform.position - transform.position) * flySpeed;
        }
    }
    public virtual void PickUp()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            player = collision.gameObject;
            canFlyToPlayer = true;
        }
    }
}
