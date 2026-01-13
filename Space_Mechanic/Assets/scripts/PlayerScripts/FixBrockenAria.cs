using UnityEngine;

public class FixBrockenAria : MonoBehaviour
{
    public bool inArea;
    public MachenLogic machine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        MachenLogic m = other.GetComponent<MachenLogic>();
        if (m)
        {
            machine = m;
            inArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        MachenLogic m = other.GetComponent<MachenLogic>();
        if (m && m == machine)
        {
            machine = null;
            inArea = false;
        }
    }
}
