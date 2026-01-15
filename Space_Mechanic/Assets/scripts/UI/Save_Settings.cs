using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SettingsData
{
    public float masterVol;
    public float gunVol;
    public float musicVol;
    public float backgroundVol;
    public float fpsLimit;
    public bool vsyncOn;
}

public class Save_Settings : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI saveText;
    private bool isSaving;
    [SerializeField] float totalSavingTime;
    private float savingtimer;

    [Header("Sliders + buttons")]
    [SerializeField] Slider masterVolum;
    [SerializeField] Slider gunsVolum;
    [SerializeField] Slider musicVolum;
    [SerializeField] Slider backgroundVolum;
    [SerializeField] Slider fpsLimiter;
    [SerializeField] Toggle_VSync toggleVsync;

    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/settings.json";

        isSaving = false;

        savingtimer = totalSavingTime;

        LoadSettings();
    }

    void Update()
    {
        if (isSaving && savingtimer <= 0)
        {
            saveText.text = ("Saved");
            isSaving = false;
        }else if (isSaving && savingtimer > 0)
        {
            saveText.text = ("Saving...");
            savingtimer -= Time.deltaTime;
        }
    }

    public void OnMyButtonClick()
    {
        if (!isSaving)
        {
            isSaving = true;
            savingtimer = totalSavingTime;

            SaveSettings();
        }
    }

    void SaveSettings()
    {
        SettingsData data = new SettingsData
        {
            masterVol = masterVolum.value,
            gunVol = gunsVolum.value,
            musicVol = musicVolum.value,
            backgroundVol = backgroundVolum.value,
            fpsLimit = fpsLimiter.value,
            vsyncOn = toggleVsync.isToggleOn
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Settings saved to: " + filePath);
    }

    void LoadSettings()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SettingsData data = JsonUtility.FromJson<SettingsData>(json);

            masterVolum.value = data.masterVol;
            gunsVolum.value = data.gunVol;
            musicVolum.value = data.musicVol;
            backgroundVolum.value = data.backgroundVol;
            fpsLimiter.value = data.fpsLimit;
            toggleVsync.isToggleOn = data.vsyncOn;

            Debug.Log("Settings Loaded!");
        }
        else
        {
            Debug.Log("No settings file found. Using defaults.");
        }
    }
}
