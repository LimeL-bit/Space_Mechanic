using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 10000;
    [SerializeField] List<GameObject> machines = new List<GameObject>();
    [SerializeField] TextMeshProUGUI notifier;

    [SerializeField] List<AudioSource> audio = new List<AudioSource>();
    private float health;

    public bool hasBrokenMachine = false;
    private void Start()
    {
        health = maxHealth;
        notifier.enabled = false;
    }

    private void Update()
    {
        BreakMachine();

        if (hasBrokenMachine)
        {
            notifier.enabled = true;
        }
        else
        {
            notifier.enabled = false;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= 100;
    }

    private void BreakMachine()
    {
        if(health <= 0 && !hasBrokenMachine)
        {
            hasBrokenMachine = true;

            int randomNumber = Random.Range(0, machines.Count);

            for (int index = 0; index < machines.Count; index++)
            {
                if(randomNumber == index)
                {
                    machines[index].GetComponent<MachenLogic>().isBroken = true;
                    notifier.text = "The " + machines[index].name + " is broken";
                    audio[index].Play();
                }
            }
        }
    }

    public void FixedMachine()
    {
        health = maxHealth;
        hasBrokenMachine = false;
    }
}
