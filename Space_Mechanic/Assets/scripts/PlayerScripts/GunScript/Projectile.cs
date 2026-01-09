using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class Projectile : MonoBehaviour
{
    [Header("General config")]
    [SerializeField] bool isPlayer;
    [SerializeField] bool showGun;

    [Header("Bullet config")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletCartage;
    [SerializeField] int bulletAmmount;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletCartrageSpeed;
    [SerializeField] int bulletSpred;
    [SerializeField] float bulletLifetime;
    [SerializeField] bool SemiToggle;
    private float angle;

    [Header("Gun placment config")]
    [SerializeField] float gudDistansFromPlayer;
    private float gunAngle;
    private float normalisedAngle;
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
        faceGun();
        gun();

        if(showGun == true){
                gameObject.SetActive(true);
        } else if(showGun == false){
                gameObject.SetActive(false);
        }    
    }

    void gun()
    {
        if (SemiToggle == true)
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
                    Rigidbody2D rbdc = bc.GetComponent<Rigidbody2D>();
                    rbdc.linearVelocity = bc.transform.right * -bulletCartrageSpeed;

                    Destroy(b, bulletLifetime);
                    Destroy(bc, bulletLifetime * 2);
                }
            }
        }
        else if (SemiToggle == false)
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
                    Rigidbody2D rbdc = bc.GetComponent<Rigidbody2D>();
                    rbdc.linearVelocity = bc.transform.right * -bulletCartrageSpeed;

                    Destroy(b, bulletLifetime);
                    Destroy(bc, bulletLifetime * 2);
                }
            }
        }
    }

    void faceGun()
    {
        if(isPlayer == true)
        {
            // Get mouse direction
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mouse - transform.position;

            // Get angle in degrees
            gunAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // Rotate the gun
            transform.rotation = Quaternion.Euler(0, 0, gunAngle - 90); // adjust -90 if sprite points up

            // Flip the gun sprite if aiming left
            normalisedAngle = (gunAngle + 360) % 360;

            if (normalisedAngle > 180) // left side
                transform.localScale = new Vector3(1, -1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);


            // Offset from player
            transform.localPosition = new Vector3(gudDistansFromPlayer, 0, 0);
        }
    }
}
