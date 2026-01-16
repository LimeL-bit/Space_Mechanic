using System;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsLoader : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Optional: FPS / VSync")]
    [SerializeField] private bool applyFPSandVSync = true;

    private string filePath;

    private void Awake()
    {
        // Make this persistent if needed
        DontDestroyOnLoad(gameObject);

        filePath = Application.persistentDataPath + "/settings.json";

        LoadAndApplySettings();
    }

    private void LoadAndApplySettings()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("No settings file found. Using defaults.");
            return;
        }

        string json = File.ReadAllText(filePath);
        SettingsData data = JsonUtility.FromJson<SettingsData>(json);

        SetMixerVolume("MasterVol", data.masterVol);
        SetMixerVolume("MusicVol", data.musicVol);
        SetMixerVolume("VoiceVol", data.VoiceVol);
        SetMixerVolume("BackgroundVol", data.backgroundVol);

        if (applyFPSandVSync)
        {
            QualitySettings.vSyncCount = data.vsyncOn ? 1 : 0;
            Application.targetFrameRate = data.vsyncOn ? -1 : (int)data.fpsLimit;
        }


        Debug.Log("Settings loaded and applied!");
    }

    private void SetMixerVolume(string param, float value)
    {
        float vol = Mathf.Max(value / 100f, 0.001f); // Avoid log(0)
        audioMixer.SetFloat(param, Mathf.Log10(vol) * 20f);
    }
}
