using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragRotate : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 target;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Debug.Log("X: " + Input.GetAxis("Mouse X") + " Y: " + Input.GetAxis("Mouse Y"));
            //transform.RotateAround(target, Vector3.right, -Input.GetAxis("Mouse Y") * speed * Time.deltaTime);
            transform.RotateAround(target, Vector3.up, Input.GetAxis("Mouse X") * speed * Time.deltaTime);
        }
    }
}
