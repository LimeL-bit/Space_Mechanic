using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    [SerializeField] private string level;
    [SerializeField] private bool notForMainMenu = false;

    private bool playerIsNearby = false;
    PlayerMovement PM;

    private void Update()
    {
        if (notForMainMenu)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerIsNearby)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            playerIsNearby = true;
            PM = collision.gameObject.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            playerIsNearby = false;
            PM = null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
