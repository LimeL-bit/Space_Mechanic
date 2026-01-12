using System;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float speed;
    [SerializeField] private float zoomTime = 3;
    private float timer;
    private bool isZoomedOut = false;

    private bool playerIsNearby = false;
    PlayerMovement PM;

    [SerializeField] private float zoomCoolDown;
    private float nextZoom;

    [SerializeField] private Vector3 cameraTargetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.orthographicSize = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerIsNearby == false)
        {
            return;
        }
        
        ZoomInAndOut();
    }

    private void ZoomInAndOut()
    {
        if (timer > 0)
        {
            //ifall kameran är inte ut zoomad
            if (!isZoomedOut)
            {
                //timer på hur länge den ska zooma ut
                timer -= Time.deltaTime;

                //zoomar ut genom en viss hastighet och tid
                cam.orthographicSize = cam.orthographicSize + speed * Time.deltaTime;

                //åker till en bestämd position
                cam.transform.position += (cameraTargetPosition - cam.transform.position) / 16;

                //gör så att man inte kan röra sig
                PM.enabled = false;

                //stannar när storleken är 8
                if (cam.orthographicSize > 8)
                {
                    cam.orthographicSize = 8; // Max size
                    isZoomedOut = true;
                }
            }
            //ifall kameran är ut zoomad
            else if (isZoomedOut)
            {
                //timer på hur länge den ska zooma in
                timer -= Time.deltaTime;

                //zoomar in genom en viss hastighet och tid
                cam.orthographicSize = cam.orthographicSize - speed * Time.deltaTime;

                //åker till den originela positionen
                cam.transform.position += (new Vector3(0, 0, -10) - cam.transform.position) / 16;

                //gör så att man röra sig igen
                PM.enabled = true;

                //stannar när storleken är 5
                if (cam.orthographicSize < 5)
                {
                    cam.orthographicSize = 5; // Min size
                    isZoomedOut = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextZoom)
        {
            timer = zoomTime;
            nextZoom = Time.time + zoomCoolDown;
        }
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
}