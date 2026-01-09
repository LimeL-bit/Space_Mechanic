using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.orthographicSize = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            cam.orthographicSize = cam.orthographicSize + 1*Time.deltaTime;
            if(cam.orthographicSize > 8)
            {
                cam.orthographicSize = 8; // Max size
            }
        }
        
    }
}
