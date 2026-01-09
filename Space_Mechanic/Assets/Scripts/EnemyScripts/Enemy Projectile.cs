using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 5f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}

