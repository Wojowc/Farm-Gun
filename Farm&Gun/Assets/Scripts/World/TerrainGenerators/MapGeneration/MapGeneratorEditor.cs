#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = (MapGenerator)target;
        base.OnInspectorGUI();

        if(GUILayout.Button("Generate map"))
        {
            mapGenerator.CreateMap();
        }
    }
}

#endif