using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


[Serializable]
public class SettingsData
{
    public float masterVol;
    public float VoiceVol;
    public float musicVol;
    public float backgroundVol;
    public float fpsLimit;
    public bool vsyncOn;
}

public class Save_Settings : MonoBehaviour
{
    public static Save_Settings Instance;

    [Header("UI Saving Display")]
    [SerializeField] TextMeshProUGUI saveText;
    private bool isSaving;
    [SerializeField] float totalSavingTime = 1f;
    private float savingtimer;

    [Header("Sliders + buttons")]
    [SerializeField] Slider masterVolum;
    [SerializeField] Slider voiceVolum;
    [SerializeField] Slider musicVolum;
    [SerializeField] Slider backgroundVolum;
    [SerializeField] Slider fpsLimiter;
    [SerializeField] Toggle_VSync toggleVsync;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    private string filePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        filePath = Application.persistentDataPath + "/settings.json";
        isSaving = false;
        savingtimer = totalSavingTime;

        LoadSettings();     // load JSON values
        ApplySettings(); ;
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
            ApplySettings(); // Apply right after saving
        }
    }

    void SaveSettings()
    {
        SettingsData data = new SettingsData
        {
            masterVol = masterVolum.value,
            VoiceVol = voiceVolum.value,
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
            voiceVolum.value = data.VoiceVol;
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

    public void ApplySettings()
    {
        float Master = Mathf.Max(masterVolum.value / 100f, 0.001f);
        float Music = Mathf.Max(musicVolum.value / 100f, 0.001f);
        float Voice = Mathf.Max(voiceVolum.value / 100f, 0.001f);
        float Background = Mathf.Max(backgroundVolum.value / 100f, 0.001f);

        audioMixer.SetFloat("MasterVol", Mathf.Log10(Master) * 20f);
        audioMixer.SetFloat("MusicVol", Mathf.Log10(Music) * 20f);
        audioMixer.SetFloat("VoiceVol", Mathf.Log10(Voice) * 20f);
        audioMixer.SetFloat("BackgroundVol", Mathf.Log10(Background) * 20f);

        QualitySettings.vSyncCount = toggleVsync.isToggleOn ? 1 : 0;
        Application.targetFrameRate = toggleVsync.isToggleOn ? -1 : (int)fpsLimiter.value;

        Debug.Log("Settings Applied!");
    }
}
