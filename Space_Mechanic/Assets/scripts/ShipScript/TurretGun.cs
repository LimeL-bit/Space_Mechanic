using System.Runtime.CompilerServices;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    private bool gunIsFliped;
    private float gunAngle;
    private float normalisedAngle;
    private float angle;
    private float fireCooldown;

    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletHole1;
    [SerializeField] GameObject bulletHole2;
    [SerializeField] int bulletAmmount;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletSpred;
    [SerializeField] float bulletLifetime;
    [SerializeField] float fireRate;

    void Start()
    {
        
    }

    void Update()
    {
        FaceMouse();
        GunColdown();
    }

    void GunColdown()
    {
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (fireCooldown <= 0f)
        {
            Shot();
            fireCooldown = fireRate;
        }
    }

    void Shot()
    {
        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < bulletAmmount; i++)
            {
                angle = Random.Range(-bulletSpred, bulletSpred);
                GameObject b1 = Instantiate(bullet, bulletHole1.transform.position, transform.rotation * Quaternion.Euler(0, 0, angle - 90));
                GameObject b2 = Instantiate(bullet, bulletHole2.transform.position, transform.rotation * Quaternion.Euler(0, 0, angle - 90));
                Rigidbody2D rbd1 = b1.GetComponent<Rigidbody2D>();
                Rigidbody2D rbd2 = b2.GetComponent<Rigidbody2D>();
                rbd1.linearVelocity = b1.transform.up * bulletSpeed;
                rbd2.linearVelocity = b2.transform.up * bulletSpeed;


                Destroy(b1, bulletLifetime);
                Destroy(b2, bulletLifetime);
            }
        }
    }

    void FaceMouse()
    {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mouse - transform.position;

            gunAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, gunAngle);

            normalisedAngle = (gunAngle + 360) % 360;
            Vector3 localScale = transform.localScale;

            if (mouse.x < transform.position.x)
            {
                localScale.y = -Mathf.Abs(localScale.y);
                gunIsFliped = true;
            }
            else
            {
                localScale.y = Mathf.Abs(localScale.y);
                gunIsFliped = false;
            }

            transform.localScale = localScale;
    }
}
