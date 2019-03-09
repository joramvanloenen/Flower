using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlowerGenerator))]
public class FlowerGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FlowerGenerator myScript = (FlowerGenerator)target;
        if (GUILayout.Button("Generate Flower"))
        {
            myScript.Generate();
        }
    }
}
