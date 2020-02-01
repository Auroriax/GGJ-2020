using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class PickUpAble : MonoBehaviour
{
    public Rigidbody rb;
    public SwapObjectManager SwapManager;
    public Outline outline;

    private void Reset()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        SwapManager = FindObjectOfType<SwapObjectManager>();
        outline = this.gameObject.GetComponent<Outline>();
    }

    // Start is called before the first frame update
    void Start()
    {
        outline.OutlineWidth = 0f;
        if (SwapManager == null)
        {
            Debug.LogWarning("No swapmanager ref assigned to this instance, attempting to automatically assign", this);
            SwapManager = FindObjectOfType<SwapObjectManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Control swapping cog wheels
        if (SwapManager.raycastSuccess && SwapManager.hit.collider.gameObject == this.gameObject && Input.GetMouseButtonDown(0))
        {
            if (SwapManager.CurrentlySelectedObject == null)
            {
                //Select this gameobject
                SwapManager.CurrentlySelectedObject = this.gameObject;
                Debug.Log("Selected: " + transform.name);
            }
            else
            {
                if (SwapManager.CurrentlySelectedObject == this.gameObject)
                {
                    Debug.Log("Deselected: " + transform.name);
                    SwapManager.CurrentlySelectedObject = null;
                }
                else
                {
                    //Swap objects around
                    Debug.Log("Swapping: " + transform.name);
                    var swapWith = SwapManager.CurrentlySelectedObject;
                    var goTo = swapWith.transform.position;
                    var rotateTo = swapWith.transform.localEulerAngles;

                    swapWith.transform.position = this.transform.position;
                    swapWith.transform.localEulerAngles = this.transform.localEulerAngles;
                    this.transform.position = goTo;
                    this.transform.localEulerAngles = rotateTo;

                    SwapManager.stopAllAngularMotion();

                    SwapManager.CurrentlySelectedObject = null;
                }
            }
        }

        //Set outline
        var somethingSelected = (SwapManager.CurrentlySelectedObject != null);
        var amISelected = (SwapManager.CurrentlySelectedObject == this.gameObject);
        var hovered = SwapManager.raycastSuccess && SwapManager.hit.collider.gameObject == this.gameObject;

        if (amISelected)
        {
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineColor = new Color(1f, 0f, 0f);
            outline.OutlineWidth = 2f;
        }
        else
        if (hovered)
        {
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineColor = new Color(1f, 1f, 1f);
            outline.OutlineWidth = 5f;
        }
        else
        if (somethingSelected)
        {
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineColor = new Color(1f, 1f, 1f);
            outline.OutlineWidth = 2f;
        }
        else
        {
            outline.OutlineWidth = 0f;
        }
    }
}
