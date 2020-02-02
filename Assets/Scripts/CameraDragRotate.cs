using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CameraDragRotate : MonoBehaviour
{

    public Vector3 target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float keyboardMultiplier = 25f;

    float x = 0.0f;
    float y = 0.0f;

    public bool inited = false;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        distance = Vector3.Distance(this.transform.position, target);
    }

    void LateUpdate()
    {
        Vector3 angles = transform.eulerAngles;
        x = ClampAngle(angles.y);
        y = ClampAngle(angles.x);

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            x += Input.GetAxis("Horizontal") * keyboardMultiplier * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Vertical") * keyboardMultiplier * ySpeed * 0.02f;
        }
        else
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target;

        if (Input.GetMouseButton(1) || Input.GetMouseButton(2) || Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f || !inited)
        {
            transform.rotation = rotation;
            transform.position = position;
            inited = true;
        }
    }

    public static float ClampAngle(float angle)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return angle;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}