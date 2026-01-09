using System;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float speed;
    [SerializeField] private float zoomTime = 3;
    private float timer;
    private bool isZoomedOut = false;

    [SerializeField] private Vector3 cameraTargetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.orthographicSize = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            if (!isZoomedOut)
            {
                //timer på hur länge den ska zooma ut
                timer -= Time.deltaTime;
                //zoomar ut genom en viss hastighet och tid
                cam.orthographicSize = cam.orthographicSize + speed * Time.deltaTime;

                //åker till en bestämd postion
                cam.transform.position += (cameraTargetPosition - cam.transform.position) / 16;
                //stannar när storleken är 8
                if (cam.orthographicSize > 8)
                {
                    cam.orthographicSize = 8; // Max size
                    isZoomedOut = true;
                    print("is zoomed out");
                }
            }
            else if (isZoomedOut)
            {
                //timer på hur länge den ska zooma in
                timer -= Time.deltaTime;
                //zoomar ut genom en viss hastighet och tid
                cam.orthographicSize = cam.orthographicSize - speed * Time.deltaTime;

                //åker till den originela postionens
                cam.transform.position += (new Vector3(0,0,-10) - cam.transform.position) / 16;
                //stannar när storleken är 5
                if (cam.orthographicSize < 5)
                {
                   cam.orthographicSize = 5; // Min size
                   isZoomedOut = false;
                    print("isnt zoomed out");
                }   
                
            }
        
        }

        ZoomOut();
        //ZoomIn();
           
    }

    private void ZoomOut()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            timer = zoomTime;
        }
    }

    private void ZoomIn()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timer = zoomTime;
        }
    }

}
