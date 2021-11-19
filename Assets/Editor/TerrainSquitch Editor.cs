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

        if (GUILayout.Button("Undersea Mountains Landscape"))
            squitch.MountainsLandscape();

        if (GUILayout.Button("Clear"))
            squitch.Clear();

        if (GUILayout.Button("Pip"))
            squitch.Pip();

        if (GUILayout.Button("Set Elevation"))
            squitch.SetElevation();

        if (GUILayout.Button("Extrude Box"))
            squitch.ExtrudeBox();

        if (GUILayout.Button("Three Stairs"))
            squitch.ThreeStairs();

        if (GUILayout.Button("Hundred Stairs"))
            squitch.HundredStairs();

        if (GUILayout.Button("Extrude Cylinder"))
            squitch.ExtrudeCylinder();

        if (GUILayout.Button("Layer Cake"))
            squitch.LayerCake();

        if (GUILayout.Button("Random Independent Profile"))
            squitch.RandomIndependentProfile();

        if (GUILayout.Button("Random Walk"))
            squitch.RandomWalkProfile();

        if (GUILayout.Button("Single Step"))
            squitch.SingleStep();

        if (GUILayout.Button("Many Steps"))
            squitch.ManySteps();

        if (GUILayout.Button("City Landscape"))
            squitch.CityLandscape();

        if (GUILayout.Button("Valley Landscape"))
            squitch.ValleyLandscape();

        if (GUILayout.Button("Fill Water"))
            squitch.InstallWater();

        if (GUILayout.Button("Fill Niche"))
            squitch.FillNiche();

        if (GUILayout.Button("Smooth"))
            squitch.Smooth();

        if (GUILayout.Button("Texture Terrain"))
            squitch.TextureTerrain();
    }
}
