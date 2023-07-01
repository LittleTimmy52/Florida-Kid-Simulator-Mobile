using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnManager6 : MonoBehaviour
{
    public GameObject testTingToSpawn;

    public float spreadXTest = 50;
    public float spreadYTest = 0;
    public float spreadZTest = 50;

    // Start is called before the first frame update
    void Start()
    {
        SpreadTestItem();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            SpreadTestItem();
        }
    }

    void SpreadTestItem()
    {
        Vector3 testRandPos = new Vector3(Random.Range(-spreadXTest, spreadXTest), Random.Range(-spreadYTest, spreadYTest), Random.Range(-spreadZTest, spreadZTest)) + transform.position;
        GameObject testClone = Instantiate(testTingToSpawn, testRandPos, Quaternion.Euler(0, Random.Range(0, 359), 0));
    }
}
