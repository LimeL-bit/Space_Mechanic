using UnityEngine;

public class FixBrockenAria : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D othere)
    {
        if (othere.CompareTag("FixAriea"))
        {

        }
    }

    private void OnCollisionExit2D(Collision2D othere)
    {
        if (othere.CompareTag("FixAriea"))
        {

        }
    }
}
