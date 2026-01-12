using UnityEngine;
using System.Collections;

public class FreezeFrame : MonoBehaviour
{
    public static FreezeFrame Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StopTime(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(StopTimeCoroutine(duration));
    }
    private IEnumerator StopTimeCoroutine(float duration)
    {
        Time.timeScale = 0f;
        // MUST use Realtime because timeScale is 0
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}


