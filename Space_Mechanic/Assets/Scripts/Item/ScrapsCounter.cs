using TMPro;
using UnityEngine;

public class ScrapsCounter : MonoBehaviour
{
    private int scrapsCount = 0;
    private TextMeshProUGUI textMeshPro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = "Scraps: " + scrapsCount;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "Scraps: " + scrapsCount;
    }

    public void AddScraps(int amount)
    {
        scrapsCount += amount;
        print("added");
    }

    public bool RemoveScraps(int amount)
    {
        if(amount <= scrapsCount)
        {
            scrapsCount -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
