using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] int bulletAmmount;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletSpred;
    [SerializeField] bool SemiToggle;

    private float bulletSpredMax;
    private float bulletSpredMin;
    private float angle;

    Rigidbody2D rbBullet;
    void Start()
    {
        bulletSpredMax = bulletSpred;
        bulletSpredMin = -bulletSpred;

        rbBullet = bullet.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SemiToggle == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < bulletAmmount; i++)
                {
                    angle = Random.Range(bulletSpredMin, bulletSpredMax);
                    GameObject b = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, angle));
                    Rigidbody2D rbd = b.GetComponent<Rigidbody2D>();
                    rbd.linearVelocity = b.transform.up * bulletSpeed;
                }
            }
        }else if(SemiToggle == false)
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i < bulletAmmount; i++)
                {
                    angle = Random.Range(bulletSpredMin, bulletSpredMax);
                    GameObject b = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, angle));
                    Rigidbody2D rbd = b.GetComponent<Rigidbody2D>();
                    rbd.linearVelocity = b.transform.up * bulletSpeed;
                }
            }
        }
    }
}
