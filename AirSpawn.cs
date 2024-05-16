using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSpawn : MonoBehaviour
{
    public GameObject[] airObjects;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnObstacle();
            maxSpawnDelay = Random.Range(1.5f, 4f);
            curSpawnDelay = 0;
        }
    }

    void SpawnObstacle()
    {
        int ranObstacle = Random.Range(0, 1);
        int spawnPoint = 0;
        Instantiate(airObjects[ranObstacle],
                    spawnPoints[spawnPoint].position,
                    spawnPoints[spawnPoint].rotation);
    }
}
