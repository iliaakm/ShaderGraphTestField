using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShaderSelector : MonoBehaviour
{
    [SerializeField]
    private Material[] materials;
    [SerializeField]
    private Renderer targetRenderer;

    public string[] MaterialsNames
    {
        get
        {
            return materials.Select(x => x.shader.name).ToArray();
        }
    }

    public void DisplayMaterial(Material material)
    {
        if (material != targetRenderer.sharedMaterial)
            targetRenderer.sharedMaterial = material;
    }

    public void DisplayMaterial(int index)
    {
        if (index < 0 || index >= materials.Length)
            return;
        DisplayMaterial(materials[index]);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ShaderSelector)), CanEditMultipleObjects]
class ShaderSelectorEditor : Editor
{
    int index = 0;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ShaderSelector shaderSelector = target as ShaderSelector;
        
        index = EditorGUILayout.Popup("Select Shader to Display", index, shaderSelector.MaterialsNames);
        shaderSelector.DisplayMaterial(index);
    }
}
#endif
