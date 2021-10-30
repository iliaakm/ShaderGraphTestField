using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToMaterial : MonoBehaviour
{
    [SerializeField]
    private Material material;
    [SerializeField]
    private string mousePositionProperty;
    [SerializeField]
    Vector3 mousePosition;
    [SerializeField]
    Vector4 materialVector;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        materialVector = new Vector4(mousePosition.x, mousePosition.y, 0, 0);
        material.SetVector(mousePositionProperty, materialVector);
    }
}
