using UnityEngine;

public class MachenLogic : MonoBehaviour
{
    [Header("General config")]
    public string toolToFix;
    public bool isBroken = true;

    [Header("scripts")]
    [SerializeField] pickupTool player;
    [SerializeField] FixBrockenAria area;

    [Header("sprites")]
    [SerializeField] Sprite fixedMachen;
    [SerializeField] Sprite brockenMachen;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        LogicForLooks();
        logic();
    }

    void LogicForLooks()
    {
        if (isBroken)
        {
            spriteRenderer.sprite = brockenMachen;
        }
        else if(!isBroken)
        {
            spriteRenderer.sprite = fixedMachen;
        }
    }

    void logic()
    {
        if (!isBroken) return;

        if (area.inArea && player.isHoldingTool && Input.GetKeyDown(KeyCode.F))
        {
            if (player.currentTool.name == toolToFix)
            {
                isBroken = false;
            }
        }
    }
}
