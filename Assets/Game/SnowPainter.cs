using UnityEngine;

public class SnowPainter : MonoBehaviour
{
    [SerializeField]
    private PainterTrigger painterTrigger;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            painterTrigger.JetOn();
        }

        if (Input.GetMouseButtonUp(0))
        {
            painterTrigger.JetOff();
        }
    }
}