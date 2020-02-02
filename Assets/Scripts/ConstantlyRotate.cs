using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ConstantlyRotate : MonoBehaviour
{
    public Rigidbody rb;
    [Tooltip("Positive is clockwise, negative is counter-clockwise.")]
    public float torque = 200f;

    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddTorque( transform.up * torque);
    }
}
