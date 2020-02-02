using UnityEngine;

public class CameraAutoRotate : MonoBehaviour
{
    public float speed = 50f;
    public Vector3 target;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target, Vector3.down, speed * Time.deltaTime);
    }
}
