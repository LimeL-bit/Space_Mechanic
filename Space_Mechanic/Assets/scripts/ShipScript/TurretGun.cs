using System.Runtime.CompilerServices;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    private bool gunIsFliped;
    private float gunAngle;
    private float normalisedAngle;

    void Start()
    {
        
    }

    void Update()
    {
        FaceMouse();
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
                localScale.x = -Mathf.Abs(localScale.x);
                gunIsFliped = true;
            }
            else
            {
                localScale.x = Mathf.Abs(localScale.x);
                gunIsFliped = false;
            }

            transform.localScale = localScale;
    }
}
