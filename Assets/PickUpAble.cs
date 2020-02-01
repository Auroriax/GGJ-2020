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

                    if (rb)
                    {
                        stopRigidBodyRotation(rb);
                    }

                    var otherrb = swapWith.GetComponent<Rigidbody>();
                    if (otherrb)
                    {
                        stopRigidBodyRotation(otherrb);
                    }

                    SwapManager.CurrentlySelectedObject = null;
                }
            }
        }

        //Set outline
        if (SwapManager.raycastSuccess)
        {
            if (SwapManager.hit.collider.gameObject == this.gameObject)
            {
                outline.OutlineWidth = 5f;
            }
            else
            {
                outline.OutlineWidth = 0f;
            }
        }

    }

    void stopRigidBodyRotation(Rigidbody rib)
    {
        rib.angularVelocity = Vector3.zero;
        rib.freezeRotation = true;

        rib.freezeRotation = false;
        rib.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
}
