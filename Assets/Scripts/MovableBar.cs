using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
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
    private Outline outline;

    private bool selected = false;
    private bool hovered = false;

    private float hoverWidth = 5f;
    private float selectedWidth = 3f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0f;
    }

    private void OnMouseEnter()
    {
        if (selected)
        {
            outline.OutlineWidth = selectedWidth;
        }
        else
        {
            outline.OutlineWidth = hoverWidth;
        }
        hovered = true;
    }

    private void OnMouseExit()
    {
        hovered = false;
        if (!selected)
        {
            outline.OutlineWidth = 0f;
        }
        else
        {
            outline.OutlineWidth = selectedWidth;
        }
    }

    void OnMouseDown()
    {
        //This grabs the position of the object in the world and turns it into the position on the screen
        gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //Sets the mouse pointers vector3
        mousePreviousLocation = GetMousePos();
        outline.OutlineWidth = selectedWidth;
        selected = true;
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

    private void OnMouseUp()
    {
        selected = false;
        if (hovered)
        {
            outline.OutlineWidth = hoverWidth;
        }
        else
        {
            outline.OutlineWidth = 0f;
        }
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
