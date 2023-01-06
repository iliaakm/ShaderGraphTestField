using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    public Material material;
    public float maxStrength = 0.1f;
    public float duration = 1f;
    public float fil = 0.5f;
    public bool pingpong = true;
    public float value;

    [ContextMenu("Impact")]
    private void Impact() => StartCoroutine(Wave());

    IEnumerator Wave()
    {
        StartCoroutine(TweenValue(0, maxStrength, duration));
        yield return new WaitForSeconds(duration);
        StartCoroutine(TweenValue(maxStrength, 0, duration));
    }

    private void Start()
    {
        TweenValue(0, maxStrength, duration);
    }

    private void Update()
    {
        material.SetVector("_FillAmount", fil * Vector4.one);

        if (pingpong)
        {
            if (value <= -maxStrength)
                TweenValue(-maxStrength, maxStrength, duration);
            if (value >= maxStrength)
                TweenValue(maxStrength, -maxStrength, duration);
        }
    }

    IEnumerator TweenValue(float start, float end, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            value = Mathf.Lerp(start, end, t);
            material.SetFloat("_WobbleX", value);
            material.SetFloat("_WobbleZ", value);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
