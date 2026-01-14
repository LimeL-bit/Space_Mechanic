using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Toggle_VSync : MonoBehaviour
{
    private bool isToggleOn;
    [SerializeField] TextMeshProUGUI buttonText;

    private void Start()
    {
        isToggleOn = false;
    }

    private void Update()
    {
        if (isToggleOn)
        {
            buttonText.text = ("On");
        }else if (!isToggleOn)
        {
            buttonText.text = ("Off");
        }
    }

    public void OnMyButtonClick()
    {
        Debug.Log("Button clicked!");

        if(isToggleOn)
        {
            isToggleOn = false;
        }else if (!isToggleOn)
        {
            isToggleOn = true;
        }
    }
}
