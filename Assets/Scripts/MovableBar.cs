using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class MovableBar : MonoBehaviour
{
    public float ForceMultiplier = 0.005f;
    public MoveDirection MoveDirection = MoveDirection.AutoDetect;
    public bool SwapForceDirection = false;
    public bool SwapMouseAx = false;

    private Vector3 gameObjectSreenPoint;
    private Vector3 mousePreviousLocation;
    private Vector3 mouseCurLocation;
    private new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        //This grabs the position of the object in the world and turns it into the position on the screen
        gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //Sets the mouse pointers vector3
        mousePreviousLocation = GetMousePos();
    }

    void OnMouseDrag()
    {
        mouseCurLocation = GetMousePos();

        var force = mouseCurLocation - mousePreviousLocation;
        if (SwapForceDirection)
            force = -force;
        rigidbody.velocity += force * ForceMultiplier;

        mousePreviousLocation = mouseCurLocation;
    }

    public Vector3 GetMousePos()
    {
        var mouseX = Input.mousePosition.x;
        var mouseY = Input.mousePosition.y;
        if (SwapMouseAx)
        {
            var temp = mouseX;
            mouseX = mouseY;
            mouseY = temp;
        }

        if (MoveDirection == MoveDirection.X)
            return new Vector3(mouseX, gameObjectSreenPoint.y, mouseY);
        else if (MoveDirection == MoveDirection.Y)
            return new Vector3(mouseY, mouseX, gameObjectSreenPoint.z);
        else if (MoveDirection == MoveDirection.Z)
            return new Vector3(gameObjectSreenPoint.x, mouseY, mouseX);
        return new Vector3(mouseX, gameObjectSreenPoint.y, mouseY);
    }
}

public enum MoveDirection
{
    AutoDetect = 0,
    X,
    Y,
    Z
}
