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
        mousePosition.z = Camera.main.farClipPlane;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        materialVector = new Vector4(worldPoint.x, worldPoint.y, 0, 0);
        material.SetVector(mousePositionProperty, materialVector);
    }
}
