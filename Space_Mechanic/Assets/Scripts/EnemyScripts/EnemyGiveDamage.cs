using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private Animator animator;

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

    [Header("Animation Settings")]
    [Tooltip("Animator Trigger parameter for death")]
    public string deathTrigger = "death"; // must match Animator Trigger
    public string attackTrigger = "attack";

    private bool isDead = false;

    void Start()
    {
        currentHealth = enemyMaxHealth;
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("EnemyDamage: No Animator component found on " + gameObject.name);
        }
    }

    // -------------------------
    // DAMAGE FUNCTION
    // -------------------------
    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (DamageTaken != null)
            DamageTaken.Play();

        Debug.Log(gameObject.name + " took damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
            OnEnemyDeath();
        }
    }

    // -------------------------
    // DAMAGE PLAYER
    // -------------------------
    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDead) return;

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

    // -------------------------
    // DEATH FUNCTION
    // -------------------------
    private void Die()
    {
        if (isDead) return;

        isDead = true;

        // Trigger the death animation using the Animator
        if (animator != null)
        {
            animator.SetTrigger(deathTrigger);
        }

        // Spawn loot if assigned
        if (scrapPrefab != null)
        {
            Instantiate(scrapPrefab, transform.position, Quaternion.identity);
        }

        // Optional Freeze Frame effect
        if (FreezeFrame.Instance != null)
        {
            FreezeFrame.Instance.StopTime(0.05f);
        }

        // Destroy the enemy after death animation finishes
        Destroy(gameObject, 1.5f); // adjust this to match your animation length
    }

    // -------------------------
    // ENEMY DEATH EFFECT
    // -------------------------
    public void OnEnemyDeath()
    {
        if (FreezeFrame.Instance != null)
        {
            FreezeFrame.Instance.StopTime(0.4f);
        }
    }
}
