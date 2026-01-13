using UnityEngine;

public class EnemyFighterAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject firingPart;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate = 1.5f;

    private float nextShootTime;
    
    public void Fire(Vector2 direction, float angle)
    {
        if(Time.time > nextShootTime)
        {
            nextShootTime = Time.time + fireRate;

            GameObject bulletClone = Instantiate(bullet, firingPart.transform.position, Quaternion.Euler(0, 0, angle));
            
            bulletClone.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * bulletSpeed;
        }
    }
}
