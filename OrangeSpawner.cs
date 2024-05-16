using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector3 spawnValuesOrangeOne;
    public Vector3 spawnValuesOrangeTwo;

    public GameObject Plane;

    public float spawnWait;
    public int startWait;
    public float speed;

    public bool stop;

    private int randEnemy;
    private Vector3 spawnPosition;

    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    void Update()
    {
        Plane.transform.position -= transform.right * Time.deltaTime * speed;
    }

    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            randEnemy = UnityEngine.Random.Range(0, 15);

            switch (randEnemy)
            {
                case 0:
                    spawnPosition = spawnValuesOrangeOne;
                    break;

                case 1:
                    spawnPosition = spawnValuesOrangeTwo;
                    break;
            }

            Instantiate (enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation, Plane.transform);

            spawnWait = UnityEngine.Random.Range(0.7f, 1.2f);

            yield return new WaitForSeconds(spawnWait);
        }
    }
}