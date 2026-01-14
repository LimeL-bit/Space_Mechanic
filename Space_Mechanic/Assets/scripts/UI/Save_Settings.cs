using TMPro;
using UnityEditor.Overlays;
using UnityEngine;

public class Save_Settings : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI saveText;
    private bool isSaveing;
    [SerializeField] float tottalSavingTime;
    private float savingtimer;


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
