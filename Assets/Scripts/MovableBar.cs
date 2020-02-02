﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
public class MovableBar : MonoBehaviour
{
    public float ForceMultiplier = 0.005f;
    
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
        mousePreviousLocation = new Vector3(Input.mousePosition.x, gameObjectSreenPoint.y, Input.mousePosition.y);
        outline.OutlineWidth = selectedWidth;
        selected = true;
    }

    void OnMouseDrag()
    {
        mouseCurLocation = new Vector3(Input.mousePosition.x, gameObjectSreenPoint.y, Input.mousePosition.y);
        var force = mouseCurLocation - mousePreviousLocation;//Changes the force to be applied
        mousePreviousLocation = mouseCurLocation;

        rigidbody.velocity += force * ForceMultiplier;
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
}

//{  // Move with drag
//    private Vector3 screenPoint;
//    private Vector3 offset;
//    private RigidbodyConstraints rigidbodyConstraints;

//    private void Start()
//    {
//        rigidbodyConstraints = GetComponent<Rigidbody>().constraints;
//    }

//    void OnMouseDown()
//    {
//        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

//        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
//    }

//    void OnMouseDrag()
//    {
//        var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
//        var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

//        var x = transform.position.x;
//        var y = transform.position.y;
//        var z = transform.position.z;
//        if ((rigidbodyConstraints & RigidbodyConstraints.FreezePositionX) == 0)
//            x = curPosition.x;
//        if ((rigidbodyConstraints & RigidbodyConstraints.FreezePositionY) == 0)
//            y = curPosition.y;
//        if ((rigidbodyConstraints & RigidbodyConstraints.FreezePositionZ) == 0)
//            z = curPosition.z;

//        transform.position = new Vector3(x, y, z);
//    }
//}


