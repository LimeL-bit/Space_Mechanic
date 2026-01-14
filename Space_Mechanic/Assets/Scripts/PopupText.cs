using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] string text;
    void Start()
    {
        textMeshProUGUI.enabled = false;
        textMeshProUGUI.text = text;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            textMeshProUGUI.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            textMeshProUGUI.enabled = false;
        }
    }
}
