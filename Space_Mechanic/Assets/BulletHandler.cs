using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageAmount = 25; // Set how much damage the gun does here

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Look for the EnemyDamage script on the thing we hit
        EnemyDamage enemy = collision.GetComponent<EnemyDamage>();

        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount);
            Destroy(gameObject); // Destroy bullet so it doesn't pass through
        }
    }
}


