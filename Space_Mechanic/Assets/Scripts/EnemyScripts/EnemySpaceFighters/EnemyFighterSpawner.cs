using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterSpawner : MonoBehaviour
{
    [Header("Fighter")]
    [SerializeField] GameObject fighter;
    [SerializeField] GameObject target;
    [SerializeField] List<GameObject> orbitPath = new List<GameObject>();

    [Header("Other")]
    [SerializeField] List<GameObject> spawns = new List<GameObject>();
    [SerializeField] int squadCount = 2;
    [SerializeField] float minimumSpawnTime = 10;
    [SerializeField] float maximumSpawnTime = 30;
    [SerializeField] GameObject notifier;
    [SerializeField] AudioSource audio;
    private float nextSpawn;

    void Start()
    {
        nextSpawn = Time.time + Random.Range(minimumSpawnTime, maximumSpawnTime + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            SpawnFighter(spawns[Random.Range(0, spawns.Count)]);
            nextSpawn = Time.time + Random.Range(minimumSpawnTime, maximumSpawnTime + 1);
        }
    }

    private void SpawnFighter(GameObject spawn)
    {
        audio.Play();
        for(int i = 0; i < squadCount; i++)
        {
            GameObject fighterClone = Instantiate(fighter, new Vector2(spawn.transform.position.x + i * 2, spawn.transform.position.y + i * 2), Quaternion.identity, notifier.transform);
            fighterClone.GetComponent<EnemyFighterAI>().AssignTarget(target);
            fighterClone.GetComponent<EnemyFighterAI>().AssignOrbitPath(orbitPath);
        }
    }
}
