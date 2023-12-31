using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject objToRotate;
    public float rotateSpeed = -20;

    // Update is called once per frame
    void Update()
    {
        objToRotate.transform.Rotate(new Vector3(0, rotateSpeed, 0) * Time.deltaTime);
    }
}
