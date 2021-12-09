using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSquitch : MonoBehaviour
{
    [Header("Elevation Settings")]
    public float SetElevation_Elevation;

    [Header("Box Settings")]
    public int Box_zMin = 30;
    public int Box_zMax = 100;
    public int Box_xMax = 100;
    public int Box_xMin = 30;
    public float Box_Height;

    [Header("Water Settings")]
    public int InstallWater_WaterLevel;
    public GameObject WaterPrefab;
    public float WaterPrefabSize;
    public Transform WaterParent;

    [Header("Smoothing Settings")]
    public int smoothLevel = 2;
    public float smoothHeight;


    [Header("Niche Flower Settings")]
    public Niche FillNiche_Niche;
    public Transform FillNiche_ParentTransform;

    [Header("Niche Coral Settings")]
    public Niche FillNiche_Niche2;
    public Transform FillNiche_ParentTransform2;



    //clear
    public void Clear()
    {
        SetElevation_Elevation = 0;
        SetElevation();

        foreach (Transform child in FillNiche_ParentTransform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        foreach (Transform child in FillNiche_ParentTransform2)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        
        foreach (Transform child in WaterParent)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        
    }

    //water
    public void InstallWater()
    {
        Terrain thisTerrian = GetComponent<Terrain>();
        if (thisTerrian == null)
            throw new System.Exception("TerrainSquitch requires a Terrain. Add a terrain to " + gameObject.name);

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrian.terrainData.heightmapResolution;
        heightMapLength = thisTerrian.terrainData.heightmapResolution;
        Debug.Log(heightMapWidth + heightMapLength);

        float heightMapWidthInWorld, heightMapLengthInWorld;
        heightMapWidthInWorld = heightMapWidth * thisTerrian.terrainData.heightmapScale.x;
        heightMapLengthInWorld = heightMapLength * thisTerrian.terrainData.heightmapScale.z;


        Vector3 worldPos;
        worldPos = new Vector3(0, InstallWater_WaterLevel, 0);
        for (worldPos.z = 0; worldPos.z < heightMapLengthInWorld; worldPos.z += WaterPrefabSize)
        {
            for (worldPos.x = 0; worldPos.x < heightMapWidthInWorld; worldPos.x += WaterPrefabSize)
            {
                worldPos.y = thisTerrian.SampleHeight(worldPos);
                if (worldPos.y < InstallWater_WaterLevel)
                    Instantiate(WaterPrefab, new Vector3(worldPos.x, InstallWater_WaterLevel, worldPos.z), Quaternion.identity, WaterParent);
            }
        }

    }

    //niche
    public void FillNiche()
    {
        Terrain thisTerrian = GetComponent<Terrain>();
        if (thisTerrian == null)
            throw new System.Exception("TerrainSquitch requires a Terrain. Add a terrain to " + gameObject.name);

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrian.terrainData.heightmapResolution;
        heightMapLength = thisTerrian.terrainData.heightmapResolution;
        Debug.Log(heightMapWidth + heightMapLength);

        float heightMapWidthInWorld, heightMapLengthInWorld;
        heightMapWidthInWorld = heightMapWidth * thisTerrian.terrainData.heightmapScale.x;
        heightMapLengthInWorld = heightMapLength * thisTerrian.terrainData.heightmapScale.z;

        Vector3 worldPos;
        worldPos = Vector3.zero;
        for (worldPos.z = 0; worldPos.z < heightMapLengthInWorld; worldPos.z += 1)
        {
            for (worldPos.x = 0; worldPos.x < heightMapWidthInWorld; worldPos.x += 1)
            {
                worldPos.y = thisTerrian.SampleHeight(worldPos);

                if (worldPos.x > FillNiche_Niche.MinX && worldPos.x < FillNiche_Niche.MaxX &&
                    worldPos.z > FillNiche_Niche.MinZ && worldPos.z < FillNiche_Niche.MaxZ &&
                    worldPos.y > FillNiche_Niche.MinElev && worldPos.y < FillNiche_Niche.MaxElev)
                {
                    if (Random.value < FillNiche_Niche.ProbabilityPerMeter)
                        Instantiate(FillNiche_Niche.NicheOccupant, worldPos, Quaternion.identity, FillNiche_ParentTransform);
                }
            }
        }
    }

    //niche 2 coral
    public void FillNicheCoral()
    {
        Terrain thisTerrian = GetComponent<Terrain>();
        if (thisTerrian == null)
            throw new System.Exception("TerrainSquitch requires a Terrain. Add a terrain to " + gameObject.name);

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrian.terrainData.heightmapResolution;
        heightMapLength = thisTerrian.terrainData.heightmapResolution;
        Debug.Log(heightMapWidth + heightMapLength);

        float heightMapWidthInWorld, heightMapLengthInWorld;
        heightMapWidthInWorld = heightMapWidth * thisTerrian.terrainData.heightmapScale.x;
        heightMapLengthInWorld = heightMapLength * thisTerrian.terrainData.heightmapScale.z;

        Vector3 worldPos;
        worldPos = Vector3.zero;
        for (worldPos.z = 0; worldPos.z < heightMapLengthInWorld; worldPos.z += 1)
        {
            for (worldPos.x = 0; worldPos.x < heightMapWidthInWorld; worldPos.x += 1)
            {
                worldPos.y = thisTerrian.SampleHeight(worldPos);

                if (worldPos.x > FillNiche_Niche2.MinX && worldPos.x < FillNiche_Niche2.MaxX &&
                    worldPos.z > FillNiche_Niche2.MinZ && worldPos.z < FillNiche_Niche2.MaxZ &&
                    worldPos.y > FillNiche_Niche2.MinElev && worldPos.y < FillNiche_Niche2.MaxElev)
                {
                    if (Random.value < FillNiche_Niche2.ProbabilityPerMeter)
                        Instantiate(FillNiche_Niche2.NicheOccupant, worldPos, Quaternion.identity, FillNiche_ParentTransform2);
                }
            }
        }
    }

    public void SetElevation()
    {
        Terrain thisTerrian = GetComponent<Terrain>();
        if (thisTerrian == null)
            throw new System.Exception("TerrainSquitch requires a Terrain. Add a terrain to " + gameObject.name);

        int heightMapWidth, heightMapHeight;
        heightMapWidth = thisTerrian.terrainData.heightmapResolution;
        heightMapHeight = thisTerrian.terrainData.heightmapResolution;
        Debug.Log(heightMapWidth + heightMapHeight);

        float[,] heights;
        heights = thisTerrian.terrainData.GetHeights(0, 0, heightMapWidth, heightMapHeight);

        Vector3 mapPos;
        mapPos.z = 0;
        for (mapPos.z = 0; mapPos.z < heightMapHeight; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < heightMapHeight; mapPos.x++)
            {
                heights[(int)mapPos.z, (int)mapPos.x] = SetElevation_Elevation;
            }
        }

        thisTerrian.terrainData.SetHeights(0, 0, heights);
    }

    public void ExtrudeBox()
    {
        Terrain thisTerrian = GetComponent<Terrain>();
        if (thisTerrian == null)
            throw new System.Exception("TerrainSquitch requires a Terrain. Add a terrain to " + gameObject.name);

        int heightMapWidth, heightMapHeight;
        heightMapWidth = thisTerrian.terrainData.heightmapResolution;
        heightMapHeight = thisTerrian.terrainData.heightmapResolution;
        Debug.Log(heightMapWidth + heightMapHeight);

        float[,] heights;
        heights = thisTerrian.terrainData.GetHeights(0, 0, heightMapWidth, heightMapHeight);

        Vector3 mapPos;
        mapPos.z = 0;
        for (mapPos.z = 0; mapPos.z < heightMapHeight; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < heightMapHeight; mapPos.x++)
            {
                if (mapPos.z > Box_zMin && mapPos.z < Box_zMax && mapPos.x > Box_xMin && mapPos.x < Box_xMax)
                {
                    heights[(int)mapPos.z, (int)mapPos.x] = Box_Height;
                }
            }
        }

        thisTerrian.terrainData.SetHeights(0, 0, heights);
    }

    //smooth
    public void Smooth()
    {
        Terrain thisTerrian = GetComponent<Terrain>();
        if (thisTerrian == null)
            throw new System.Exception("TerrainSquitch requires a Terrain. Add a terrain to " + gameObject.name);

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrian.terrainData.heightmapResolution;
        heightMapLength = thisTerrian.terrainData.heightmapResolution;
        Debug.Log(heightMapWidth + heightMapLength);

        float[,] heights;
        heights = thisTerrian.terrainData.GetHeights(0, 0, heightMapWidth, heightMapLength);

        Vector3 mapPos;
        mapPos.z = 0;
        for (int i = 0; i < smoothLevel; i++)
        {
            for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
            {
                for (mapPos.x = 0; mapPos.x < heightMapWidth; mapPos.x++)
                {
                    if (mapPos.x > 1 && mapPos.z > 1 && mapPos.x < heightMapWidth - 1 && mapPos.z < heightMapWidth - 1)
                    {
                        float smoothHeight = (heights[(int)mapPos.z, (int)mapPos.x] + heights[(int)mapPos.z + 1, (int)mapPos.x] + heights[(int)mapPos.z - 1, (int)mapPos.x] + heights[(int)mapPos.z, (int)mapPos.x + 1] + heights[(int)mapPos.z + 1, (int)mapPos.x - 1]) / 5;
                        heights[(int)mapPos.z, (int)mapPos.x] = smoothHeight;
                    }
                }
            }
        }
        thisTerrian.terrainData.SetHeights(0, 0, heights);
    }
}
