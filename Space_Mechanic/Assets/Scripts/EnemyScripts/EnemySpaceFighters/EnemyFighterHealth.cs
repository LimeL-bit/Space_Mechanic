using Unity.VisualScripting;
using UnityEngine;

public class EnemyFighterHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    private int randomNumber;

    private void Start()
    {
        randomNumber = Random.Range(0, 2); ;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 25)
        {
            if (randomNumber == 0)
            {
                GetComponent<EnemyFighterAI>().ChangeState(EnemyFighterAI.State.retreat);
            }
            else
            {
                GetComponent<EnemyFighterAI>().ChangeState(EnemyFighterAI.State.kamikaze);
            }
        }
    }
}
