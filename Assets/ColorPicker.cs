using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] [RequireComponent(typeof(Renderer))]
public class ColorPicker : MonoBehaviour
{
    public Color selectedColor = Color.blue;
    public Renderer rendererComponent;

    // Start is called before the first frame update
    void Start()
    {
        if (!rendererComponent)
            rendererComponent = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        rendererComponent.material.color = selectedColor;
    }
}