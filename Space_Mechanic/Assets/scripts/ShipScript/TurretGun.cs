using System.Collections.Generic;
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
    [SerializeField] List<Sprite> turretSprites = new List<Sprite>();
    [SerializeField] bool isUpsideDown = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FaceMouse();
        GunColdown();
    }

    void GunColdown()
    {
        if (Time.time > fireCooldown)
        {
            Shot();
            fireCooldown = Time.time + fireRate;
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
                //localScale.y = -Mathf.Abs(localScale.y);
                gunIsFliped = true;
            }
            else
            {
                //localScale.y = Mathf.Abs(localScale.y);
                gunIsFliped = false;
            }

            transform.localScale = localScale;
        SpriteRotator(gunAngle);
    }

    private void SpriteRotator(float angle)
    {
        if (!isUpsideDown)
        {
            if (gunAngle <= 0 && gunAngle > -90)
            {
                spriteRenderer.sprite = turretSprites[0];
            }
            else if (gunAngle > 0 && gunAngle < 22.5)
            {
                spriteRenderer.sprite = turretSprites[1];
            }
            else if (gunAngle > 22.5 && gunAngle < 45)
            {
                spriteRenderer.sprite = turretSprites[2];
            }
            else if (gunAngle > 45 && gunAngle < 90)
            {
                spriteRenderer.sprite = turretSprites[3];
            }
            else if (gunAngle > 90 && gunAngle < 112.5)
            {
                spriteRenderer.sprite = turretSprites[4];
            }
            else if (gunAngle > 112.5 && gunAngle < 135)
            {
                spriteRenderer.sprite = turretSprites[5];
            }
            else if (gunAngle > 135 && gunAngle < 180)
            {
                spriteRenderer.sprite = turretSprites[6];
            }
            else if (gunAngle > -180 && gunAngle < -90)
            {
                spriteRenderer.sprite = turretSprites[6];
            }
        }
        else
        {
            if (gunAngle >= 0 && gunAngle < 90)
            {
                spriteRenderer.sprite = turretSprites[0];
            }
            else if (gunAngle > -22.5 && gunAngle < 0)
            {
                spriteRenderer.sprite = turretSprites[1];
            }
            else if (gunAngle > -45 && gunAngle < -22.5)
            {
                spriteRenderer.sprite = turretSprites[2];
            }
            else if (gunAngle > -90 && gunAngle < -45)
            {
                spriteRenderer.sprite = turretSprites[3];
            }
            else if (gunAngle > -112.5 && gunAngle < -90)
            {
                spriteRenderer.sprite = turretSprites[4];
            }
            else if (gunAngle > -135 && gunAngle < -112.5)
            {
                spriteRenderer.sprite = turretSprites[5];
            }
            else if (gunAngle > -180 && gunAngle < -135)
            {
                spriteRenderer.sprite = turretSprites[6];
            }
            else if (gunAngle > -180 && gunAngle > 90)
            {
                spriteRenderer.sprite = turretSprites[6];
            }
        }
    }
}
