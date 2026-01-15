using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    // Component references
    private Animator animator;
    private Transform player;

    // General settings
    [Header("Settings")]
    public bool canShoot = true;
    public GameObject projectilePrefab;
    public Transform firePoint;

    // Shooting stats
    [Header("Shooting Stats")]
    public float fireRate = 2f;
    private float nextFireTime = 0f;

    // Range settings
    [Header("Range Settings")]
    public float shootRange = 10f;

    // Animation settings
    [Header("Animation Settings")]
    public float shootDelay = 0.1f;

    // Death settings
    [Header("Death Settings")]
    public float deathDuration = 1.5f;

    // Internal state
    private bool isDead = false;
    private bool isShooting = false;

    // Animator parameter names (must match Animator exactly)
    private const string ATTACK_TRIGGER = "attack";
    private const string DEATH_BOOL = "death";

    void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("EnemyShooting: No Animator found on this GameObject!");
        }
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("EnemyShooting: No GameObject with tag 'Player' found in scene.");
        }
    }

    void Update()
    {
        if (isDead || player == null || !canShoot)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootRange && Time.time >= nextFireTime && !isShooting)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (isDead) return;

        isShooting = true;

        // Trigger Idle -> Attack transition
        animator.SetTrigger(ATTACK_TRIGGER);

        // Spawn projectile after short delay
        StartCoroutine(FireProjectileWithDelay(shootDelay));
    }

    private IEnumerator FireProjectileWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!isDead && projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }

        isShooting = false;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        canShoot = false;
        player = null;

        // Trigger Any State -> Death
        animator.SetBool(DEATH_BOOL, true);

        Destroy(gameObject, deathDuration);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
