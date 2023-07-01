using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnManager1 : MonoBehaviour
{
    public GameObject testEnemy;
    private float testSpawnRange = 50;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GenerateSpawnPosition()
    {
        // for getting a random rotation
        float spawnPosX = Random.Range(-testSpawnRange, testSpawnRange);
        float spawnPosZ = Random.Range(-testSpawnRange, testSpawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    public void SpawnEnemy()
    {
        Instantiate(testEnemy, GenerateSpawnPosition(), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
    }
}
