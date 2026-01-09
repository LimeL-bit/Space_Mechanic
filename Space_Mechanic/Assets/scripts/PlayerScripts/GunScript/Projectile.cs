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
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletCartrageSpeed;
    [SerializeField] int bulletSpred;
    [SerializeField] float bulletLifetime;
    [SerializeField] bool SemiToggle;
    private float fireCooldown = 0f;
    private float angle;
    private bool gunIsFliped;

    [Header("Gun placment config")]
    [SerializeField] float gunDistansFromPlayer;
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
        gunColdown();

        if (showGun == true){
                gameObject.SetActive(true);
        } else if(showGun == false){
                gameObject.SetActive(false);
        }    
    }

    void gunColdown()
    {
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (fireCooldown <= 0f && SemiToggle == false)
        {
            gun();
            fireCooldown = fireRate;
        }else if(SemiToggle == true)
        {
            gun();
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
                    if(gunIsFliped == false){
                        rbdc.linearVelocity = bc.transform.right * -bulletCartrageSpeed;
                    }
                    else
                    {
                        rbdc.linearVelocity = bc.transform.right * bulletCartrageSpeed;
                    }


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
                    if (gunIsFliped == false)
                    {
                        rbdc.linearVelocity = bc.transform.right * -bulletCartrageSpeed;
                    }
                    else
                    {
                        rbdc.linearVelocity = bc.transform.right * bulletCartrageSpeed;
                    }

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
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mouse - transform.position;

            gunAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, gunAngle - 90);

            normalisedAngle = (gunAngle + 360) % 360;
            Vector3 localScale = transform.localScale;

            if (normalisedAngle > 90 && normalisedAngle < 270)
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

            transform.localPosition = new Vector3(gunDistansFromPlayer, 0, 0);
        }
    }
}
