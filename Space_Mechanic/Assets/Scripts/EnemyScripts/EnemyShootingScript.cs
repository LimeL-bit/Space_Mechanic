using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    private Animator animator;
    private Transform player;

    [Header("Settings")]
    public bool canShoot = true;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Shooting Stats")]
    public float fireRate = 2f;
    private float nextFireTime;

    [Header("Range Settings")]
    public float shootRange = 10f;

    [Header("Animation Settings")]
    public string idleAnimationName = "Idle";
    public string shootAnimationName = "Shoot";
    public string deathAnimationName = "Death";
    public float shootDelay = 0.1f;

    private bool isDead = false;
    private bool isShooting = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        PlayIdle();
    }

    void Update()
    {
        if (isDead || !canShoot || player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootRange && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        else if (!isShooting)
        {
            // Only play idle if not currently shooting
            PlayIdle();
        }
    }

    void Shoot()
    {
        if (animator != null && !string.IsNullOrEmpty(shootAnimationName))
        {
            animator.Play(shootAnimationName, 0, 0f);
        }

        isShooting = true;
        StartCoroutine(FireProjectileWithDelay(shootDelay));
        // Reset shooting state after animation length (assume 0.5s here, adjust to your clip)
        StartCoroutine(ResetShootingState(0.5f));
    }

    private IEnumerator FireProjectileWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isDead && projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }

    private IEnumerator ResetShootingState(float duration)
    {
        yield return new WaitForSeconds(duration);
        isShooting = false;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        canShoot = false;
        player = null;

        if (animator != null && !string.IsNullOrEmpty(deathAnimationName))
        {
            animator.Play(deathAnimationName, 0, 0f);
        }

        Destroy(gameObject, 1.0f); // adjust to match Death animation length
    }

    private void PlayIdle()
    {
        if (animator != null && !string.IsNullOrEmpty(idleAnimationName))
        {
            animator.Play(idleAnimationName, 0, 0f);
        }
    }
}
