using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float ZoomOut;
    [SerializeField] private float speed;
    [SerializeField] private float zoomTime = 3;
    private float timer;
    private bool isZoomedOut = false;

    private bool playerIsNearby = false;
    PlayerMovement PM;
    Rigidbody2D playerRigidBody;

    [SerializeField] private float zoomCoolDown;
    private float nextZoom;

    [SerializeField] private Vector3 cameraTargetPosition;

    [SerializeField] List<TurretGun> turrets = new List<TurretGun>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.orthographicSize = 5;

        foreach(TurretGun turret in turrets)
        {
            turret.enabled = false;
        }
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

                //gör så att man inte kan röra sig men man kan använda turrets
                playerRigidBody.linearVelocity = Vector2.zero;
                PM.enabled = false;
                foreach (TurretGun turret in turrets)
                {
                    turret.enabled = true;
                }

                //stannar när storleken stämmer med max zoom
                if (cam.orthographicSize > ZoomOut)
                {
                    cam.orthographicSize = ZoomOut; // Max size
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

                //gör så att man röra sig igen och inte kunna använda turrets
                PM.enabled = true;
                foreach (TurretGun turret in turrets)
                {
                    turret.enabled = false;
                }

                //stannar när storleken stämmer med min zoom 
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
            playerRigidBody = collision.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            playerIsNearby = false;
            PM = null;
            playerRigidBody = null;
        }
    }
}