using UnityEngine;
using System.Collections;

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
                    StartCoroutine(ShowArmorTemporarily(visual, 5f));
                }
                playerHealth.isArmored = true;
                isPickedUp = true;
                Debug.Log("Armor equipped!");
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator ShowArmorTemporarily(Transform armorVisual, float duration)
    {
        armorVisual.gameObject.SetActive(true);  
        yield return new WaitForSeconds(duration); 
        armorVisual.gameObject.SetActive(false); 
    }
}



