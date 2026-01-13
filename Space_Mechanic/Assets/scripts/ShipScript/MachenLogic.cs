using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachenLogic : MonoBehaviour
{
    [Header("General config")]
    public string toolToFix;
    public bool isBroken = false;
    [SerializeField] float timeBeforeDestruction = 15;
    private float timer;
    [SerializeField] string gameOverScene;

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
        timer = timeBeforeDestruction;
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

            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                print("GameOver");
                SceneManager.LoadScene(gameOverScene);
            }
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
                timer = timeBeforeDestruction;
                FindAnyObjectByType<ShipHealth>().FixedMachine();
            }
        }
    }

}
