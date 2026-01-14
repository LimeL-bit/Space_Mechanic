using TMPro;
using UnityEngine;

public class EnemyNotifier : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    void Start()
    {
        textMeshProUGUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount > 0)
        {
            textMeshProUGUI.enabled = true;
        }
        else
        {
            textMeshProUGUI.enabled = false;
        }
    }
}
