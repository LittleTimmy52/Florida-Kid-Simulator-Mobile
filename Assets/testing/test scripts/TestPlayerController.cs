using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestPlayerController : MonoBehaviour
{
    private float TPYVal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horInput, 0f, verInput);
        Vector3 moveDestination = transform.position + movement;
        GetComponent<NavMeshAgent>().destination = moveDestination;

        transform.rotation = Quaternion.Euler(0, TPYVal, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            TPYVal -= 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            TPYVal += 1;
        }
    }
}
