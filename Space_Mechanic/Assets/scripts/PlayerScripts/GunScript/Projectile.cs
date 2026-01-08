using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletCartage;
    [SerializeField] int bulletAmmount;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletSpred;
    [SerializeField] float bulletLifetime;
    [SerializeField] bool SemiToggle;
    private float angle;

    Rigidbody2D rbBullet;
    Rigidbody2D rbBulletcartage;
    void Start()
    {
        rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBulletcartage = bulletCartage.GetComponent<Rigidbody2D>();
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
                    angle = Random.Range(bulletSpred, -bulletSpred);
                    GameObject b = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, angle));
                    Rigidbody2D rbd = b.GetComponent<Rigidbody2D>();
                    rbd.linearVelocity = b.transform.up * bulletSpeed;

                    GameObject bc = Instantiate(bulletCartage, transform.position, transform.rotation * Quaternion.Euler(0, 0, angle));
                    Rigidbody2D rbdc = b.GetComponent<Rigidbody2D>();
                    rbdc.linearVelocity = b.transform.right * -bulletSpeed/5;

                    Destroy(b, bulletLifetime);
                    Destroy(bc, bulletLifetime * 2);
                }
            }
        }else if(SemiToggle == false)
        {
            if (Input.GetMouseButton(0))
            {
                for (int i = 0; i < bulletAmmount; i++)
                {
                    angle = Random.Range(bulletSpred, -bulletSpred);
                    GameObject b = Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 0, angle));
                    Rigidbody2D rbd = b.GetComponent<Rigidbody2D>();
                    rbd.linearVelocity = b.transform.up * bulletSpeed;

                    GameObject bc = Instantiate(bulletCartage, transform.position, transform.rotation * Quaternion.Euler(0, 0, angle));
                    Rigidbody2D rbdc = b.GetComponent<Rigidbody2D>();
                    rbdc.linearVelocity = b.transform.right * -bulletSpeed / 5;

                    Destroy(b, bulletLifetime);
                    Destroy(bc, bulletLifetime * 2);
                }
            }
        }
    }
}
