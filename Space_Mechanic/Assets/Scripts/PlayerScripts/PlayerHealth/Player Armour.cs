using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    private bool isPickedUp = false;
    private Transform playerTransform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Enable damage reduction
                playerHealth.isArmored = true;

                // Teleport to player and become a child so it follows them
                isPickedUp = true;
                playerTransform = other.transform;
                transform.SetParent(playerTransform);
                transform.localPosition = Vector3.zero; // Centers armor on player

                Debug.Log("Armor equipped! Damage reduced by half.");
                Destroy(gameObject);
            }
        }
    }
}

