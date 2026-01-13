using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    [SerializeField] private string level;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GoToScene();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
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

}
