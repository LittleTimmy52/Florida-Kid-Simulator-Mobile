using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnManager5 : MonoBehaviour
{
    public GameObject[] TestItemsToPickFrom;
    public float testRaycastDist = 100f;
    public float testOverlapTestBoxSize = 1f;
    public LayerMask testSpawnedObjLayer;

    // Start is called before the first frame update
    void Start()
    {
        TestPosRaycast();
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.C))
        {
            TestPosRaycast();
        }*/
    }

    void TestPosRaycast()
    {
        RaycastHit testHit;

        if (Physics.Raycast(transform.position, Vector3.down, out testHit, testRaycastDist))
        {
            Quaternion testSpawnRotation = Quaternion.FromToRotation(Vector3.up, testHit.normal);

            Vector3 testOverlapScale = new Vector3(testOverlapTestBoxSize, testOverlapTestBoxSize, testOverlapTestBoxSize);
            Collider[] testCollidersInsideOverlapBox  = new Collider[1];
            int testNumberOfCollidersFound = Physics.OverlapBoxNonAlloc(testHit.point, testOverlapScale, testCollidersInsideOverlapBox, testSpawnRotation, testSpawnedObjLayer);

            Debug.Log("num of colliders found " + testNumberOfCollidersFound);

            if (testNumberOfCollidersFound == 0)
            {
                Debug.Log("spawned obj");
                Pick(testHit.point, testSpawnRotation);
            }else
            {
                Debug.Log("name of colliders found " + testCollidersInsideOverlapBox[0].name);
            }

            void Pick(Vector3 testPosToSpawn, Quaternion testRotationToSpawn)
            {
                int testRandonIndex = Random.Range(0, TestItemsToPickFrom.Length);
                GameObject clone = Instantiate(TestItemsToPickFrom[testRandonIndex], testPosToSpawn, testRotationToSpawn);
                Destroy(GameObject.Find("item spawner(Clone)"));
            }
        }
    }
}
