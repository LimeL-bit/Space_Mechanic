using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [SerializeField] int damage = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Try to find the EnemyDamage script on the object hit
        EnemyDamage enemy = collision.GetComponent<EnemyDamage>();
        EnemyFighterHealth enemyFighter = collision.GetComponent<EnemyFighterHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        if(enemyFighter != null)
        {
            enemyFighter.TakeDamage(damage);
        }

        // 2. Despawn immediately upon hitting ANY collider (Wall, Enemy, etc.)
        Destroy(gameObject);
    }
}



