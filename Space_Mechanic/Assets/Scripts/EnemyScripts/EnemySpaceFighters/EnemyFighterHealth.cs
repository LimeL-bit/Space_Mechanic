using Unity.VisualScripting;
using UnityEngine;

public class EnemyFighterHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float crashDamage = 5;
    [SerializeField] float despawnTime = 20;
    [SerializeField] GameObject explosion;

    private bool hasLowHealth = false;

    private int randomNumber;

    private void Start()
    {
        randomNumber = Random.Range(0, 2); ;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<ShipHealth>().TakeDamage(crashDamage);
            print("crash");
            Destroy(gameObject);
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 50)
        {
            if (randomNumber == 0)
            {
                GetComponent<EnemyFighterAI>().ChangeState(EnemyFighterAI.State.retreat);

                if (!hasLowHealth)
                {
                    hasLowHealth = true;
                    Destroy(gameObject, despawnTime);
                }
            }
            else
            {
                GetComponent<EnemyFighterAI>().ChangeState(EnemyFighterAI.State.kamikaze);
            }
        }

        if(health <= 0)
        {
            GameObject explosionClone = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionClone, 0.5f);

            Destroy(gameObject);
            print("vanquished");
        }
    }
}
