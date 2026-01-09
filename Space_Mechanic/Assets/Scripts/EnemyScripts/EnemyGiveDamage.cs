using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int enemyMaxHealth = 100;
    private int currentHealth; 

    [Header("Damage to Player")]
    public int damage = 10;
    public float damageCooldown = 1f;
    private float lastDamageTime;

    [Header("Loot")]
    public GameObject scrapPrefab;

    private void Start()
    {
        currentHealth = enemyMaxHealth;
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastDamageTime = Time.time;
                }
            }
        }
    }

    private void Die()
    {
        if (scrapPrefab != null)
        {
            Instantiate(scrapPrefab, transform.position, Quaternion.identity);
        }
        Debug.Log(gameObject.name + " destroyed.");
        Destroy(gameObject);
    }
}


