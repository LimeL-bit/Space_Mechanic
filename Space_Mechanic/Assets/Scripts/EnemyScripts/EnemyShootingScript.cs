using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Settings")]
    public bool canShoot = true; 
    public GameObject projectilePrefab;
    public Transform firePoint; 
    [Header("Shooting Stats")]
    public float fireRate = 2f;
    private float nextFireTime;
    void Update()
    {
        if (canShoot && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        }
    }
}

