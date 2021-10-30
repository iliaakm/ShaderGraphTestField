using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [SerializeField]
    Vector3 axe;
    [SerializeField]
    float speed;

    private void Update()
    {
        transform.Rotate(axe, speed * Time.deltaTime);
    }
}
