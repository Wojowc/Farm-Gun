using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(WaveFunctionGenerator))]
public class WFCEditor : Editor
{

    public override void OnInspectorGUI()
    {
        WaveFunctionGenerator waveFunctionCollapse = (WaveFunctionGenerator)target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Prototypes"))
        {
            waveFunctionCollapse.InitializeWaveFunction();
        }
        if (GUILayout.Button("Clear"))
        {
            waveFunctionCollapse.ClearAll();
        }
        if (GUILayout.Button("Collapse"))
        {
            waveFunctionCollapse.StartCollapse();
        }
    }
}