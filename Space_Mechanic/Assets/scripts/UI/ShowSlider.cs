using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowSlider : MonoBehaviour
{
    [SerializeField] Slider sliderToShow;
    [SerializeField] TextMeshProUGUI viewPoint;
    [SerializeField] bool isFPS;
    private int valueToShow;
    void Start()
    {
        
    }

    void Update()
    {
        valueToShow = (int) sliderToShow.value;

        if (isFPS)
        {
            viewPoint.text = "Limit: " + valueToShow;
        }else if (!isFPS)
        {
            viewPoint.text = "Volum: " + valueToShow;
        }
    }
}
