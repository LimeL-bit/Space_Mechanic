using UnityEditor;
using UnityEngine;

public class pickupTool : MonoBehaviour
{
    public bool isHoldingTool;
    public GameObject currentTool;
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
        if (isHoldingTool && currentTool != null)
        {
            currentTool.transform.position = transform.position;
        }

        TryPickupOrDrop();
    }

    void TryPickupOrDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHoldingTool && tempTool != null)
            {
                currentTool = tempTool;
                currentTool.GetComponent<Rigidbody2D>().simulated = false;
                isHoldingTool = true;
            }
            else if (isHoldingTool)
            {
                currentTool.GetComponent<Rigidbody2D>().simulated = true;
                currentTool = null;
                isHoldingTool = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D othere)
    {
        if (othere.CompareTag("Tool") )
        {
            tempTool = othere.gameObject;
            canHoldTool = true;
        }
    }

    private void OnTriggerExit2D(Collider2D othere)
    {
        if (othere.CompareTag("Tool"))
        {
            if (tempTool == othere.gameObject)
            {
                tempTool = null;
                canHoldTool = false;
            }
        }
    }
}
