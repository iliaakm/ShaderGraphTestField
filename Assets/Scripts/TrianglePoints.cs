using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePoints : MonoBehaviour
{
    [SerializeField]
    private Material material;
    [SerializeField]
    private string[] pointPropertyNames;

    private int nextPoint = 0;

    void Start()
    {
        ResetMaterial();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.farClipPlane;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector4 materialVector = new Vector4(worldPoint.x, worldPoint.y, 0, 0);
            material.SetVector(pointPropertyNames[nextPoint], materialVector);
        }

        if(Input.GetMouseButtonUp(0))
        {
            nextPoint = (nextPoint + 1) % pointPropertyNames.Length;
        }
    }

    private void OnDestroy()
    {
        ResetMaterial();
    }

    void ResetMaterial()
    {
        foreach (var property in pointPropertyNames)
        {
            material.SetVector(property, Vector4.zero);
        }
    }
}
