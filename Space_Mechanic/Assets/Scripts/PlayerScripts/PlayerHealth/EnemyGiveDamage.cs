using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int enemyMaxHealth;
    public int damage;
    public float damageCooldown = 1f;
    private float lastDamageTime;
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Enemy is touching: " + other.name + " with tag: " + other.tag);

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
}

