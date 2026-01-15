using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Toggle_VSync : MonoBehaviour
{
    public bool isToggleOn;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Save_Settings saveSettings;

    private void Start()
    {

        ApplyState();
    }

    private void ApplyState()
    {
        if (isToggleOn)
        {
            QualitySettings.vSyncCount = 1;
            buttonText.text = "V-Sync: ON";
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            buttonText.text = "V-Sync: OFF";
        }
    }

    public void OnMyButtonClick()
    {
        isToggleOn = !isToggleOn;
        ApplyState();
    }
}
