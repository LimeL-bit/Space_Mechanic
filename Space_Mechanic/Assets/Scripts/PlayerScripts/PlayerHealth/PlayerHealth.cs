using UnityEngine;
using TMPro;
using Unity.VectorGraphics; // Needed for TextMeshPro
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public AudioSource takeDamageSound;
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [HideInInspector] public bool isArmored = false;

    [Header("UI")]
    public TMP_Text healthText; // Assign in Inspector
    private bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    public void TakeDamage(int amount)
    {
        if (isArmored)
        {
            amount = Mathf.RoundToInt(amount * 0.5f);
        }

        if (isDead) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
        takeDamageSound.Play();
        
        Debug.Log("Player took damage: " + amount + " | Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        Debug.Log("Player healed: " + amount + " | Health: " + currentHealth);
    }
    void Die()
    {
        isDead = true;
        SceneManager.LoadScene(6);
    }
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth;
        }
    }
}

