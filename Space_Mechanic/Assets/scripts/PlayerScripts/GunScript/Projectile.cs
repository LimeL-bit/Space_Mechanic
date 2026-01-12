using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class Projectile : MonoBehaviour
{
    [Header("General config")]
    [SerializeField] TextMeshProUGUI ammoCounter;
    [SerializeField] bool isPlayer;
    [SerializeField] bool showGun;

    [Header("Gun config")]
    [SerializeField] float fireRate;
    [SerializeField] float relodeSpeed;
    [SerializeField] int magSize;
    [SerializeField] bool SemiToggle;
    private float relodeCooldown = 0f;
    [SerializeField] private bool isReloading = false;
    private float fireCooldown = 0f;
    private float angle;
    private bool gunIsFliped;
    [SerializeField] int currentMagSize;

    [Header("Bullet config")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletCartage;
    [SerializeField] int bulletAmmount;
    [SerializeField] int bulletSpeed;
    [SerializeField] int bulletCartrageSpeed;
    [SerializeField] int bulletSpred;
    [SerializeField] float bulletLifetime;

    [Header("Gun placment config")]
    [SerializeField] float gunDistansFromPlayer;
    private float gunDistansFromPlayerPrivate;
    private float gunAngle;
    private float normalisedAngle;
    Rigidbody2D rbBullet;
    Rigidbody2D rbBulletcartage;

    void Start()
    {
        currentMagSize = magSize;

        rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBulletcartage = bulletCartage.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FaceGun();
        GunColdown();
        Relode();

        if (showGun == true){
                gameObject.SetActive(true);
        } else if(showGun == false){
                gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R) && isReloading == false && currentMagSize < magSize)
        {
            isReloading = true;
            relodeCooldown = relodeSpeed;
        }

        if (isReloading == false)
        {
            ammoCounter.text = "Ammo Left: " + currentMagSize.ToString(); ;
        }
        else if (isReloading == true)
        {
            ammoCounter.text = "Reloding...";

        }


    }

    void GunColdown()
    {
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }

        if (fireCooldown <= 0f && SemiToggle == false)
        {
            Gun();
            fireCooldown = fireRate;
        }else if(SemiToggle == true)
        {
            Gun();
        }
    }

    void Gun()
    {
        if (currentMagSize > 0 && isReloading == false)
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
                        if (gunIsFliped == false)
                        {
                            rbdc.linearVelocity = bc.transform.right * -bulletCartrageSpeed;
                        }
                        else
                        {
                            rbdc.linearVelocity = bc.transform.right * bulletCartrageSpeed;
                        }

                        currentMagSize--;

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

                        currentMagSize--;

                        Destroy(b, bulletLifetime);
                        Destroy(bc, bulletLifetime * 2);
                    }
                }
            }
        }
    }

    void FaceGun()
    {
        if(isPlayer == true)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = mouse - transform.position;

            gunAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, gunAngle - 90);

            normalisedAngle = (gunAngle + 360) % 360;
            Vector3 localScale = transform.localScale;

            if (mouse.x < transform.parent.position.x)
            {
                localScale.x = -Mathf.Abs(localScale.x);
                gunDistansFromPlayerPrivate = -gunDistansFromPlayer;
                gunIsFliped = true;
            }
            else
            {
                localScale.x = Mathf.Abs(localScale.x);
                gunDistansFromPlayerPrivate = gunDistansFromPlayer;
                gunIsFliped = false;
            }



            transform.localScale = localScale;

            transform.localPosition = new Vector3(gunDistansFromPlayerPrivate, 0, 0);
        }
    }

    void Relode()
    {
        if (currentMagSize <= 0 && isReloading == false)
        {
            isReloading = true;
            relodeCooldown = relodeSpeed;
        }

        if (isReloading == true)
        {
            relodeCooldown -= Time.deltaTime;

            if (relodeCooldown <= 0f)
            {
                currentMagSize = magSize;
                isReloading = false;
            }
        }
    }
}