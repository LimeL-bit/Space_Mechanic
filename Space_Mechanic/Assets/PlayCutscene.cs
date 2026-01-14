using UnityEngine;
using UnityEngine.Playables;

public class PlayCutsceneOnStart : MonoBehaviour
{
    public PlayableDirector director;
    void Start()
    {
        if (director != null)
        {
            director.Play(); 
        }
    }
}

