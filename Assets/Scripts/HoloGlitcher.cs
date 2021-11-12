using System.Collections;
using UnityEngine;

public class HoloGlitcher : MonoBehaviour
{
    [SerializeField]
    private float glitchChance = 0.1f;

    private Renderer holoRenderer;
    private WaitForSeconds glitchLoopWait = new WaitForSeconds(0.1f);
    private WaitForSeconds glitchDuration = new WaitForSeconds(0.1f);

    private void Awake()
    {
        holoRenderer = GetComponent<Renderer>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);

            if (glitchTest <= glitchChance)
            {
                StartCoroutine(Glitch());
            }
            yield return glitchLoopWait;
        }
    }

    private IEnumerator Glitch()
    {
        glitchDuration = new WaitForSeconds(Random.Range(0.05f, 0.5f));
        holoRenderer.material.SetFloat("_Amount", 0.1f);
        holoRenderer.material.SetFloat("_Clip", 1f);

        yield return glitchDuration;

        holoRenderer.material.SetFloat("_Amount", 0f);
        holoRenderer.material.SetFloat("_Clip", 0.0f);
    }
}