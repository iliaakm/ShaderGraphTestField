using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToMaterial : MonoBehaviour
{
    [SerializeField]
    private Material material;
    [SerializeField]
    private string mousePositionProperty;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.farClipPlane;
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector4 materialVector = new Vector4(worldPoint.x, worldPoint.y, 0, 0);
        material.SetVector(mousePositionProperty, materialVector);
    }
}
