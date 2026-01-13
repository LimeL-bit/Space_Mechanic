using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 10000;
    [SerializeField] List<GameObject> machines = new List<GameObject>();
    private float health;

    public bool hasBrokenMachine = false;
    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        BreakMachine();
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
                    print(machines[index].name + " is broken");
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
