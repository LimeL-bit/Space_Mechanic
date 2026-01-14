using TMPro;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class Save_Settings : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI saveText;
    private bool isSaveing;
    [SerializeField] float tottalSavingTime;
    private float savingtimer;

    [Header("Sliders + buttons")]
    [SerializeField] Slider masterVolum;
    [SerializeField] Slider shotsVolum;
    [SerializeField] Slider enemyVolum;
    [SerializeField] Slider backgroundVolum;
    [SerializeField] Slider fpsLimiter;
    [SerializeField] Toggle_VSync toggleVsync;

    private void Start()
    {
        isSaveing = false;
    }

    void Update()
    {
        if (isSaveing && savingtimer <= 0)
        {
            saveText.text = ("save");
            isSaveing = false;
        }else if (isSaveing && savingtimer > 0)
        {
            saveText.text = ("Saving...");
            savingtimer -= Time.deltaTime;
        }
    }

    public void OnMyButtonClick()
    {
        if (!isSaveing)
        {
            isSaveing = true;
            savingtimer = tottalSavingTime;
        }
    }
}
