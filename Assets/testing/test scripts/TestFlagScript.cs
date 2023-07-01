using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlagScript : MonoBehaviour
{
    private Rigidbody testFlagRb;

    // Start is called before the first frame update
    void Start()
    {
        testFlagRb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        testFlagRb.isKinematic = true;
    }
}
