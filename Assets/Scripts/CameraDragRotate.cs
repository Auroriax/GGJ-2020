using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragRotate : MonoBehaviour
{
    public float speed = 1f;
    public float minZoomLevel = 500f;
    public float maxZoomLevel = 25f;
    public Vector3 target;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            //Debug.Log("X: " + Input.GetAxis("Mouse X") + " Y: " + Input.GetAxis("Mouse Y"));
            //transform.RotateAround(target, Vector3.right, -Input.GetAxis("Mouse Y") * speed * Time.deltaTime);
            RotateCamera(Input.GetAxis("Mouse X") * speed * Time.deltaTime);
        }

        if (Input.GetAxis("Horizontal") != 0f) {
            transform.RotateAround(target, Vector3.up, Input.GetAxis("Horizontal") * speed * Time.deltaTime * 0.5f);
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            Debug.Log("Changing zoom level");
            var prevPos = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, Input.mouseScrollDelta.y * 5f);
            if (Vector3.Distance(transform.position,target) <= maxZoomLevel || Vector3.Distance(transform.position, target) >= minZoomLevel)
            {
                transform.position = prevPos;
            }
        }
    }

    private void RotateCamera(float amount)
    {
        transform.RotateAround(target, Vector3.up, amount);
    }
}
