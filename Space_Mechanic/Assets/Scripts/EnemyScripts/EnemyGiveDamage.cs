using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int enemyMaxHealth = 100;
    private int currentHealth;
    public AudioSource DamageTaken;
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
        DamageTaken.Play();
        currentHealth -= amount;
        Debug.Log(gameObject.name + " took damage. Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
            OnEnemyDeath();
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
        if (FreezeFrame.Instance != null)
        {
            Debug.Log("FreezeFrame.Instance is NULL");
            FreezeFrame.Instance.StopTime(0.05f);
        }

        if (scrapPrefab != null)
        {
            Instantiate(scrapPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }



    public void OnEnemyDeath()
    {
        if (FreezeFrame.Instance != null)
        {
            FreezeFrame.Instance.StopTime(0.4f); 
        }
    }
}


