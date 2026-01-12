using UnityEngine;

public class FixBrockenAria : MonoBehaviour
{
    //FA stands for Fixing Area

    private bool isCollidingWithFA;
    private GameObject tempSavingOfFA;
    [SerializeField] pickupTool pT;
    public bool wantTpFix;

    void Start()
    {
        tempSavingOfFA = null;
    }

    void Update()
    {
        InteractWithFA();
    }

    void InteractWithFA()
    {
        if(pT.toolBeingHeald.name == tempSavingOfFA.gameObject.name && Input.GetKeyDown(KeyCode.F))
        {
                wantTpFix = true;
        }
    }

    
  
    private void OnTriggerEnter2D(Collider2D othere)
    {
        if (othere.CompareTag("FixAriea"))
        {
            Debug.Log("Enterd collision with " + othere.gameObject.name);
            tempSavingOfFA = othere.gameObject;
            isCollidingWithFA = true;
        }
    }

    private void OnTriggerExit2D(Collider2D othere)
    {
        if (othere.CompareTag("FixAriea"))
        {
            Debug.Log("Exited collision with " + othere.gameObject.name);
            tempSavingOfFA = null;
            isCollidingWithFA = false;
        }
    }
}
