using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapObjectManager : MonoBehaviour
{
    public GameObject CurrentlySelectedObject;

    [HideInInspector]
    public RaycastHit hit;
    [HideInInspector]
    public Ray ray;
    [HideInInspector]
    public bool raycastSuccess;

    private void Reset()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        raycastSuccess = Physics.Raycast(ray, out hit, 100.0f);
    }

    public void stopAllAngularMotion()
    {
        var rbs = FindObjectsOfType<Rigidbody>();

        for(var i = 0; i != rbs.Length; i += 1)
        {
            var rb = rbs[i];
            var con = rb.constraints;
            rb.angularVelocity = Vector3.zero;
            rb.freezeRotation = true;

            rb.freezeRotation = false;
            rb.constraints = con;
        }
    }
}
