using UnityEngine;

public class ShowOrHideGun : MonoBehaviour
{
    public bool showGun;
    void Start()
    {
        showGun = false;
    }

    void Update()
    {
        EquipUnequipGun();

        if(showGun == true)
        {
            gameObject.SetActive(true);
        }else if ( showGun == false)
        {
            gameObject.SetActive(false);
        }
    }

    void EquipUnequipGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && showGun == true)
        {
            showGun = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && showGun == false)
        {
            showGun = true;
        }
    }
}
