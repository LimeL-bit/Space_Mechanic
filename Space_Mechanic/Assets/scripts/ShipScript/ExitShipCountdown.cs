using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class ExitShipCountdown : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float totalTimeBeforExitAvalebul;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] bool useTimer;
    private float currentTimeLeft;
    private bool timeEnded;
    private bool playerInZone;
    public int seconds;
    public int minits;

    public Animator transition;
    public float transitionTime = 1f;
    [SerializeField] private string level;

    void Start()
    {
        playerInZone = false;
        timeEnded = false;
        currentTimeLeft = totalTimeBeforExitAvalebul;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTimeLeft <= 0)
        {
            timeEnded = true;
        } else if (timeEnded == false && useTimer == true)
        {
            currentTimeLeft -= Time.deltaTime;
        }

        if(useTimer == true)
        {
            TimerLogic();
            UpdateTimer();
        }
        TransferPlayer();
    }

    void TimerLogic()
    {
        minits = (int)currentTimeLeft / 60;

        seconds = (int)currentTimeLeft % 60;
    }

    void UpdateTimer()
    {
        if(seconds >= 10)
        {
            timer.text = minits + ":" + seconds;
        }
        else if(seconds < 10)
        {
            timer.text = minits + ":0" + seconds;
        } 
    }

    void TransferPlayer()
    {
        if (playerInZone == true && Input.GetKeyDown(KeyCode.E))
        {
            if (timeEnded == true && useTimer == true)
            {
                GoToScene();
            }
            else if (useTimer == false)
            {
                GoToScene();
            }
        }
    }
    public void GoToScene()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }

    private void OnTriggerEnter2D(Collider2D othere)
    {
        if (othere.CompareTag("Player"))
        {
            playerInZone = true;
            Debug.Log("Enterd exit zone");
        }
    }

    private void OnTriggerExit2D(Collider2D othere)
    {
        if (othere.CompareTag("Player"))
        {
            playerInZone = false;
            Debug.Log("Exited exit zone");
        }
    }
}
