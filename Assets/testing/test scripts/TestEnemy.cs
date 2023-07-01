using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour
{
    public float testLookRadious = 20f;
    Transform testTarget;
    NavMeshAgent testAgent;

    // Start is called before the first frame update
    void Start()
    {
        testAgent = GetComponent<NavMeshAgent>();
        testTarget = GameObject.Find("TestPlayer").transform;
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        float testDistance = Vector3.Distance(testTarget.position, transform.position);

        if (testDistance <= testLookRadious)
        {
            testAgent.SetDestination(testTarget.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, testLookRadious);
    }
}
