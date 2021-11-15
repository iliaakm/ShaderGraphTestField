using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowPainter : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    GameObject snowPainterVFX;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit raycastHit;
            Ray ray = new Ray(transform.transform.position, transform.forward);
            if(Physics.Raycast(ray, out raycastHit))
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, lineRenderer.transform.position);
                lineRenderer.SetPosition(1, raycastHit.point);

                snowPainterVFX.transform.position = raycastHit.point;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
        }
    }

    void HideSnowPainter()
    {

    }
}
