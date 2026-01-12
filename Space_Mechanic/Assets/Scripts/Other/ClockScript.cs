using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    void Update()
    {
        float t = Time.timeSinceLevelLoad;
        TimeSpan timeSpan = TimeSpan.FromSeconds(t);
        timerText.text = timeSpan.ToString(@"mm\:ss");
    }
}

