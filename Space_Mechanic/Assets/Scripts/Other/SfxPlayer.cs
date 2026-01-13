using UnityEngine;

public class ArmourVisualSound : MonoBehaviour
{
    public GameObject ArmourVisual; // Drag your ArmourVisual here in the inspector
    public AudioSource PickUpSFX;

    private bool hasPlayed = false;

    void Update()
    {
        if (!hasPlayed && ArmourVisual.activeSelf)
        {
            PickUpSFX.Play();
            hasPlayed = true; // Prevents it from playing repeatedly
        }
    }
}

