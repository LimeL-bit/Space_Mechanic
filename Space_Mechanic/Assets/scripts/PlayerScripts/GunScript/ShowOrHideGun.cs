using UnityEngine;

public class ShowOrHideGun : MonoBehaviour
{
    public bool showGun;
    [SerializeField] GameObject gun;


    void Start()
    {
        showGun = false;
    }

    void Update()
    {
        EquipUnequipGun();

        if(showGun == true)
        {
            gun.SetActive(true);
        }else if ( showGun == false)
        {
            gun.SetActive(false);
        }
    }

    void EquipUnequipGun()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("pressed 1");
        }

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
