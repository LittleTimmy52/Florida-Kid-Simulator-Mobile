using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnManager3 : MonoBehaviour
{
    public float testRaycastDistance = 100f;
    public GameObject testObjToSpawn;
    private float testSpawnRange = 50f;

    // Start is called before the first frame update
    void Start()
    {
        PositionRaycast();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PositionRaycast();
            Debug.Log("Enemy spawned");
        }
    }

    void PositionRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, testRaycastDistance))
        {
            SpawnEnemy();
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        // for getting a random rotation
        float spawnPosX = Random.Range(-testSpawnRange, testSpawnRange);
        float spawnPosZ = Random.Range(-testSpawnRange, testSpawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, transform.position.y, spawnPosZ);
        return randomPos;
    }

    public void SpawnEnemy()
    {
        Instantiate(testObjToSpawn, GenerateSpawnPosition(), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
    }
}
