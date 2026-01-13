using UnityEngine;

public class EnemyFighterBullet : MonoBehaviour
{
    [SerializeField] float damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<ShipHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
