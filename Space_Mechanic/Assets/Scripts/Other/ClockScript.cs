using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Required for TextMeshPro
public class Timer : MonoBehaviour
{
    public float timeRemaining = 120; // 2 minutes in seconds
    public bool timerIsRunning = false;
    public TMP_Text timeText; // Drag your TextMeshPro object here

    private void Start()
    {
        // Start the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        GetComponent<PlayerHealth>();
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                DisplayTime(0);
                SceneManager.LoadScene(6);
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Formatting seconds into MM:SS
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
