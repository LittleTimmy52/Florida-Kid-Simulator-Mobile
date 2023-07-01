using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSpawnManager2 : MonoBehaviour
{
    public float testRaycastDistance = 100f;
    public GameObject testObjToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        PositionRaycast();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PositionRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, testRaycastDistance))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            GameObject clone = Instantiate(testObjToSpawn, hit.point, spawnRotation);
        }
    }
}
