using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void OnMyButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
