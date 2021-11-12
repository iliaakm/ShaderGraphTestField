using UnityEngine;

public class DisplacementControl : MonoBehaviour
{
    [SerializeField]
    private float displacementAmount;

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime);
        meshRenderer.material.SetFloat("_Amount", displacementAmount);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            displacementAmount++;
        }
    }
}