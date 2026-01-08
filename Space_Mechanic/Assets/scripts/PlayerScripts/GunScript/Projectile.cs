using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] int bulletAmmount;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletSpred;
    private float bulletSpredMax;
    private float bulletSpredMin;
    Rigidbody2D rbBullet;
    Rigidbody2D rb;
    void Start()
    {
        bulletSpredMax = bulletSpred;
        bulletSpredMin = -bulletSpred;

        rbBullet = bullet.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for(int i=0; i < bulletAmmount; i++)
            {
                GameObject b = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, Random.Range(bulletSpredMin,bulletSpredMax)));
                Rigidbody2D rb = b.GetComponent<Rigidbody2D>();
                rb.linearVelocity = new Vector2(bulletSpeed, 0);
            }
        }
    }
}
