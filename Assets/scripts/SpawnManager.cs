using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region PRIVATE

    private float spreadX = 50f;
    private float spreadY = 20f;
    private float spreadZ = 50f;
    private int difficulty;

    #endregion

    #region PUBLIC

    public GameObject[] objectsToSpawn;
    public float raycastDist = 100f;
    public float overlapTestBoxSize = 1f;
    public LayerMask spawnedObjLayer;
    public bool spawnFlag = true;
    public bool spawnCop = true;
    public bool spawnEnabled = true;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty");
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnEnabled == true)
        {
            if (difficulty == 1 && spawnCop)
            {
                PosRaycast();
                spawnCop = true;
                PosRaycast();
            }else
            {
                PosRaycast();
            }
        }
    }

    public void PosRaycast()
    {
        RaycastHit Hit;

        if (Physics.Raycast(new Vector3(Random.Range(-spreadX, spreadX), Random.Range(-spreadY, spreadY), Random.Range(-spreadZ, spreadZ)), Vector3.down, out Hit, raycastDist))
        {
            Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, Hit.normal);

            Vector3 overlapScale = new Vector3(overlapTestBoxSize, overlapTestBoxSize, overlapTestBoxSize);
            Collider[] collidersInsideOverlapBox  = new Collider[1];
            int numberOfCollidersFound = Physics.OverlapBoxNonAlloc(Hit.point, overlapScale, collidersInsideOverlapBox, spawnRotation, spawnedObjLayer);

            Debug.Log("num of colliders found " + numberOfCollidersFound);

            if (numberOfCollidersFound == 0)
            {
                Debug.Log("spawned obj");
                Pick(Hit.point, spawnRotation);
            }else
            {
                Debug.Log("name of colliders found " + collidersInsideOverlapBox[0].name);
                PosRaycast();
            }
        }
    }
    void Pick(Vector3 posToSpawn, Quaternion rotationToSpawn)
    {
        if(spawnFlag == true && spawnCop == true)
        {
            GameObject clone = Instantiate(objectsToSpawn[0], posToSpawn, rotationToSpawn);
            spawnFlag = false;
        }else if(spawnFlag == true || spawnCop == true)
        {
            if(spawnFlag == true)
            {
                GameObject clone = Instantiate(objectsToSpawn[0], posToSpawn, rotationToSpawn);
                spawnFlag = false;
                spawnEnabled = false;
            }
            if(spawnCop == true)
            {
                GameObject clone2 = Instantiate(objectsToSpawn[1], posToSpawn, rotationToSpawn);
                spawnCop = false;
                spawnEnabled = false;
            }
        }
    }
}
