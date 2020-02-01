using UnityEngine;

[ExecuteInEditMode] 
[RequireComponent(typeof(Renderer))]
public class ColorPicker : MonoBehaviour
{
    public Material[] Materials;

    private Renderer rendererComponent;

    private void Start()
    {
        if (!rendererComponent)
            rendererComponent = GetComponent<Renderer>();
    }

    public void SelectMaterialDefault()
    {
        rendererComponent.material = Materials[0];
    }

    public void SelectMaterialGood()
    {
        rendererComponent.material = Materials[1];
    }

    public void SelectMaterialWrong()
    {
        rendererComponent.material = Materials[2];
    }
}