using UnityEngine;

public class MachenLogic : MonoBehaviour
{
    public string toolToFix;
    [SerializeField] FixBrockenAria fBA;
    [SerializeField] pickupTool tool;

    public bool isBrocken;

    void Start()
    {
        isBrocken = false;
    }

    void Update()
    {
        if(fBA.wantTpFix == true && tool.toolBeingHeald.name == toolToFix)
        {
            isBrocken = false;
            fBA.wantTpFix = false;
        }
    }
}
