using UnityEditor;
using UnityEngine;

public class pickupTool : MonoBehaviour
{
    public bool isHoldingTool;
    public GameObject toolBeingHeald;
    private GameObject tempTool;
    private bool canHoldTool;
    [SerializeField] ShowOrHideGun ShowOrHideGun;
    Rigidbody2D toolsRb;
    void Start()
    {
        isHoldingTool = false;
        canHoldTool = false;
    }

    void Update()
    {
        if(isHoldingTool == true)
        {
            Debug.Log("you are holding " + toolBeingHeald.name);
            toolBeingHeald.transform.position = transform.position;
        }

        DropAndPickupp();
    }
    void DropAndPickupp()
    {
        if (canHoldTool == true && isHoldingTool == false && Input.GetKeyDown(KeyCode.E) && ShowOrHideGun.showGun == false)
        {
            toolBeingHeald = tempTool;

            toolsRb = toolBeingHeald.GetComponent<Rigidbody2D>();
            toolsRb.simulated = false;

            isHoldingTool = true;
            return;
        }else if (Input.GetKeyDown(KeyCode.E) && isHoldingTool == true)
        {
            toolsRb.simulated = true;
            toolsRb = null;

            toolBeingHeald = null;
            isHoldingTool = false;
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D othere)
    {
        if (othere.CompareTag("Tool") )
        {
            Debug.Log("Enterd collision with " + othere.gameObject.name);
            tempTool = othere.gameObject;
            canHoldTool = true;
        }
    }

    private void OnTriggerExit2D(Collider2D othere)
    {
        if (othere.CompareTag("Tool"))
        {
            Debug.Log("Left collision with " + othere.gameObject.name);
            tempTool = null;
            canHoldTool = false;
        }
    }
}
