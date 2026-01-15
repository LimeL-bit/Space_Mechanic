using UnityEngine;
using TMPro; 

public class ShowKeycodeToPlayer : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TMP_Text textComponent; 

    [Header("Settings")]
    [TextArea(3, 10)]
    [SerializeField] private string messageToShow = "Welcome to the area!";

    void Start()
    {
        if (textComponent != null) textComponent.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textComponent.text = messageToShow;
            textComponent.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            textComponent.enabled = false;
        }
    }
}


