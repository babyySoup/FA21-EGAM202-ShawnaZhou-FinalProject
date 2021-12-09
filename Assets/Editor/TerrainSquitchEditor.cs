using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TerrainSquitch))]
[CanEditMultipleObjects]
public class TerrainSquitchEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TerrainSquitch squitch = (TerrainSquitch)target;

        if (GUILayout.Button("Clear"))
            squitch.Clear();

        if (GUILayout.Button("Set Elevation"))
            squitch.SetElevation();

        if (GUILayout.Button("Extrude Box"))
            squitch.ExtrudeBox();


        if (GUILayout.Button("Fill Water"))
            squitch.InstallWater();

        if (GUILayout.Button("Fill Niche"))
            squitch.FillNiche();

        if (GUILayout.Button("Fill Niche2"))
            squitch.FillNiche2();

        if (GUILayout.Button("Smooth"))
            squitch.Smooth();
    }
}