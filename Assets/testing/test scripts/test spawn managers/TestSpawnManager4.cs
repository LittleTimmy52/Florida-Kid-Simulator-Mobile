using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnManager4 : MonoBehaviour
{
    public float testSpreadX = 50f;
    public float testSpreadY = 0f;
    public float testSpreadZ = 50f;
    public GameObject testObjToSpawn;
    public GameObject testObjToSpawnFlag;
    public float testRaycastDistance = 100f;
    //private float testSpawnRange = 50f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTestEnemy();
        SpawnTestFlag();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PositionRaycast();
            Debug.Log("Enemy spawned");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            PositionRaycastFlag();
            Debug.Log("Flag spawned");
        }
    }

    void SpawnTestFlag()
    {
        Vector3 randomPos = new Vector3(Random.Range(-testSpreadX, testSpreadX), testSpreadY + 20, Random.Range(-testSpreadZ, testSpreadZ));
        GameObject clone = Instantiate(testObjToSpawnFlag, randomPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    public void PositionRaycastFlag()
    {
        RaycastHit hitFlag;
        if (Physics.Raycast(transform.position, Vector3.down, out hitFlag, testRaycastDistance))
        {
            SpawnTestFlag();
        }
    }

    void SpawnTestEnemy()
    {
        Vector3 randomPos = new Vector3(Random.Range(-testSpreadX, testSpreadX), Random.Range(-testSpreadY, testSpreadY), Random.Range(-testSpreadZ, testSpreadZ));
        GameObject clone = Instantiate(testObjToSpawn, randomPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    public void PositionRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, testRaycastDistance))
        {
            //SpawnEnemy();
            SpawnTestEnemy();
        }
    }

    /*private Vector3 GenerateSpawnPosition()
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
    }*/
}

/* fix test flag, it wont spawn correctley and watch how to spawn things at random
rotations and do this to both 

get a test of the menu running

revise the game and replace test stuff with actual stuff */