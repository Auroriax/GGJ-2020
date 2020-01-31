using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnObjectArrowKeys : MonoBehaviour
{
    public Rigidbody rb;
    public float torque = 0f;

    private void Awake()
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
        float turn = Input.GetAxis("Horizontal");
        rb.AddTorque( transform.up * torque * turn);
    }
}
