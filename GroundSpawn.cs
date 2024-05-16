using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    public GameObject[] groundObjects;
    public Transform[] spawnPoints;

    public float maxSpawnDelay;
    public float curSpawnDelay;

    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if(curSpawnDelay > maxSpawnDelay)
        {
            SpawnObstacle();
            maxSpawnDelay = Random.Range(1f, 3f);
            curSpawnDelay = 0;
        }
    }

    void SpawnObstacle()
    {
        int ranObstacle = Random.Range(0, 2);
        int spawnPoint = 0;
        Instantiate(groundObjects[ranObstacle],
                    spawnPoints[spawnPoint].position,
                    spawnPoints[spawnPoint].rotation);
    }
}
