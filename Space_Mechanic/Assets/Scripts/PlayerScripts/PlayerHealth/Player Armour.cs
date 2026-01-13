using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    private bool isPickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPickedUp)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Transform visual = other.transform.Find("ArmourVisual");

                if (visual != null)
                {
                    visual.gameObject.SetActive(true); 
                }
                playerHealth.isArmored = true;
                isPickedUp = true;
                Debug.Log("Armor equipped!");
                Destroy(gameObject);
            }
        }
    }
}


