using UnityEngine;
using System.Collections; // Needed for coroutines

public class EnemyShooting : MonoBehaviour
{
    private Animator animator;

    [Header("Settings")]
    public bool canShoot = true;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Shooting Stats")]
    public float fireRate = 2f;
    private float nextFireTime;

    [Header("Animation Settings")]
    public string shootAnimationName = "Shoot"; // Name of the shooting animation
    public float shootDelay = 0.1f; // Delay before projectile spawns (adjust to match animation)
    void Start()
    {
        animator = GetComponent<Animator>();
    }
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
        if (animator != null)
        {
            animator.Play(shootAnimationName, 0, 0f);
        }
        StartCoroutine(FireProjectileWithDelay(shootDelay));
    }
    private IEnumerator FireProjectileWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        }
    }
}


