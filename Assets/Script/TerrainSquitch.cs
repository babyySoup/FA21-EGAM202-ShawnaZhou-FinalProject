using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSquitch : MonoBehaviour
{
    [Header("Pip Settings")]
    public int Pip_X;
    public int Pip_Z;
    public float Pip_Height;

    [Header("Elevation Settings")]
    public float SetElevation_Elevation;

    [Header("Box Settings")]
    public int Box_zMin = 30;
    public int Box_zMax = 100;
    public int Box_xMax = 100;
    public int Box_xMin = 30;
    public float Box_Height;

    [Header("Cylinder Settings")]
    public int Radius;
    public int Cylinder_x;
    public int Cylinder_z;
    public float Cylinder_Height;

    [Header("Random Walk Profile Settings")]
    public float RandomWalk_StartingElevation;
    public float Random_MaxStepSize;

    [Header("Single Step Settings")]
    public float SingleStep_MaxStepHeight;

    [Header("Many Steps Settings")]
    public float ManySteps_MaxStepHeight;
    public float ManySteps_NSteps;

    [Header("Water Settings")]
    public int InstallWater_WaterLevel;
    public GameObject WaterPrefab;
    public float WaterPrefabSize;
    public Transform WaterParent;

    [Header("Smoothing Settings")]
    public int smoothLevel = 2;
    public float smoothHeight;


    [Header("Niche Mushroom Settings")]
    public Niche FillNiche_Niche;
    public Transform FillNiche_ParentTransform;

    [Header("Niche Coral Settings")]
    public Niche FillNiche_Niche2;
    public Transform FillNiche_ParentTransform2;

    [Header("Niche Seaweed Settings")]
    public Niche FillNiche_Niche3;
    public Transform FillNiche_ParentTransform3;

    [Header("Niche Cabbage Settings")]
    public Niche FillNiche_Niche4;
    public Transform FillNiche_ParentTransform4;

    /////////////////HOTKEYS////////////////////////////////
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            MountainsLandscape();
            GetComponent<UnityEngine.AI.NavMeshSurface>().BuildNavMesh();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            ValleyLandscape();
            GetComponent<UnityEngine.AI.NavMeshSurface>().BuildNavMesh();
        }



        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


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
        foreach (Transform child in FillNiche_ParentTransform3)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }

        foreach (Transform child in WaterParent)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        foreach (Transform child in FillNiche_ParentTransform4)
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

    //niche 3 seaweed
    public void FillNicheSeaweed()
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

                if (worldPos.x > FillNiche_Niche3.MinX && worldPos.x < FillNiche_Niche3.MaxX &&
                    worldPos.z > FillNiche_Niche3.MinZ && worldPos.z < FillNiche_Niche3.MaxZ &&
                    worldPos.y > FillNiche_Niche3.MinElev && worldPos.y < FillNiche_Niche3.MaxElev)
                {
                    if (Random.value < FillNiche_Niche3.ProbabilityPerMeter)
                        Instantiate(FillNiche_Niche3.NicheOccupant, worldPos, Quaternion.identity, FillNiche_ParentTransform3);
                }
            }
        }
    }

    //niche cabbage 
    public void FillNicheCabbage()
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

                if (worldPos.x > FillNiche_Niche4.MinX && worldPos.x < FillNiche_Niche4.MaxX &&
                    worldPos.z > FillNiche_Niche4.MinZ && worldPos.z < FillNiche_Niche4.MaxZ &&
                    worldPos.y > FillNiche_Niche4.MinElev && worldPos.y < FillNiche_Niche4.MaxElev)
                {
                    if (Random.value < FillNiche_Niche4.ProbabilityPerMeter)
                        Instantiate(FillNiche_Niche4.NicheOccupant, worldPos, Quaternion.identity, FillNiche_ParentTransform4);
                }
            }
        }
    }




    public void Pip()
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

        heights[Pip_Z, Pip_X] = Pip_Height;
        thisTerrian.terrainData.SetHeights(0, 0, heights);
        thisTerrian.terrainData.SetHeights(0, 0, heights);

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

    public void ExtrudeCylinder()
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
        for (mapPos.z = 0; mapPos.z < heightMapHeight; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < heightMapHeight; mapPos.x++)
            {
                if ((mapPos.z - Cylinder_z) * (mapPos.z - Cylinder_z) + (mapPos.x - Cylinder_x) * (mapPos.x - Cylinder_x) <= Radius * Radius)
                {
                    heights[(int)mapPos.z, (int)mapPos.x] = Cylinder_Height;
                }
            }
        }
        thisTerrian.terrainData.SetHeights(0, 0, heights);
    }

    //layer cake 
    public void LayerCake()
    {
        Cylinder_z = 50;
        Cylinder_x = 50;
        Radius = 50;
        Cylinder_Height = 0.1f;
        ExtrudeCylinder();

        Cylinder_z = 50;
        Cylinder_x = 50;
        Radius = 35;
        Cylinder_Height = 0.15f;
        ExtrudeCylinder();

        Cylinder_z = 50;
        Cylinder_x = 50;
        Radius = 20;
        Cylinder_Height = 0.2f;
        ExtrudeCylinder();

    }

    public void RandomIndependentProfile()
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
        float heightAtThisZ;
        for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
        {
            heightAtThisZ = Random.value;
            for (mapPos.x = 0; mapPos.x < heightMapLength; mapPos.x++)
            {
                heights[(int)mapPos.z, (int)mapPos.x] = heightAtThisZ;
            }
        }
        thisTerrian.terrainData.SetHeights(0, 0, heights);
    }


    public void RandomWalkProfile()
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
        float heightAtThisZ = RandomWalk_StartingElevation;
        for (mapPos.z = 0; mapPos.z < heightMapLength; mapPos.z++)
        {
            heightAtThisZ += Random.Range(-Random_MaxStepSize, Random_MaxStepSize);
            for (mapPos.x = 0; mapPos.x < heightMapLength; mapPos.x++)
            {
                heights[(int)mapPos.z, (int)mapPos.x] = heightAtThisZ;
            }
        }
        thisTerrian.terrainData.SetHeights(0, 0, heights);
    }

    public void SingleStep()
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

        Vector3 mapPos = Vector3.zero;

        Plane dividingPlane;
        Vector3 planePoint, planeNormal;
        float stepSize;

        planePoint = new Vector3(Random.Range(0, heightMapWidth), 0, Random.Range(0, heightMapLength));
        planeNormal = Random.onUnitSphere;
        dividingPlane = new Plane(planeNormal, planePoint);

        stepSize = Random.Range(-SingleStep_MaxStepHeight, SingleStep_MaxStepHeight);

        for (mapPos.x = 0; mapPos.x < heightMapLength; mapPos.x++)
        {
            for (mapPos.z = 0; mapPos.z < heightMapWidth; mapPos.z++)
            {
                if (dividingPlane.GetSide(mapPos))
                    heights[(int)mapPos.z, (int)mapPos.x] += stepSize;
            }
        }
        thisTerrian.terrainData.SetHeights(0, 0, heights);
    }

    public void ManySteps()
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

        Vector3 mapPos = Vector3.zero;

        Plane dividingPlane;
        Vector3 planePoint, planeNormal;
        float stepSize;



        for (int stepCount = 0; stepCount < ManySteps_NSteps; stepCount++)
        {
            planePoint = new Vector3(Random.Range(0, heightMapWidth), 0, Random.Range(0, heightMapLength));
            planeNormal = Random.onUnitSphere;
            dividingPlane = new Plane(planeNormal, planePoint);

            stepSize = Random.Range(-ManySteps_MaxStepHeight, ManySteps_MaxStepHeight);
            for (mapPos.x = 0; mapPos.x < heightMapLength; mapPos.x++)
            {
                for (mapPos.z = 0; mapPos.z < heightMapWidth; mapPos.z++)
                {
                    if (dividingPlane.GetSide(mapPos))
                        heights[(int)mapPos.z, (int)mapPos.x] += stepSize;
                }
            }
        }
        thisTerrian.terrainData.SetHeights(0, 0, heights);
    }



    public void ThreeStairs()
    {
        Box_zMin = 0;
        Box_zMax = 10;
        Box_xMin = 0;
        Box_xMax = 10;
        Box_Height = 0.1f;
        ExtrudeBox();

        Box_zMin = 0;
        Box_zMax = 10;
        Box_xMin = 10;
        Box_xMax = 20;
        Box_Height = 0.2f;
        ExtrudeBox();

        Box_zMin = 0;
        Box_zMax = 10;
        Box_xMin = 20;
        Box_xMax = 30;
        Box_Height = 0.3f;
        ExtrudeBox();
    }

    public void HundredStairs()
    {
        Box_zMin = 0;
        Box_zMax = 10;
        Box_xMin = 0;
        Box_xMax = 10;
        Box_Height = 0.1f;
        for (int i = 0; i < 101; i++)
        {
            ExtrudeBox();
            Box_xMin += 5;
            Box_xMax += 20;
            Box_Height += 0.01f;
        }
    }

    //city 
    public void CityLandscape()
    {
        SetElevation_Elevation = 0;
        SetElevation();

        ManySteps_MaxStepHeight = 0.2f;
        ManySteps_NSteps = 20;
        ManySteps();


        ManySteps_MaxStepHeight = 0.1f;
        ManySteps_NSteps = 60;
        ManySteps();

        ManySteps_MaxStepHeight = 0.002f;
        ManySteps_NSteps = 1000;
        ManySteps();
    }
    ////////////////// Valley//////////////////////////
    public void ValleyLandscape()
    {
        Clear();
        Clear();
        Clear();
        Clear();
        Clear();
        Clear();

        ManySteps_MaxStepHeight = 0.15f;
        ManySteps_NSteps = 20;
        ManySteps();


        ManySteps_MaxStepHeight = 0.007f;
        ManySteps_NSteps = 40;
        ManySteps();

        ManySteps_MaxStepHeight = 0.002f;
        ManySteps_NSteps = 100;
        ManySteps();

        Smooth();
        Smooth();
        Smooth();

        TextureValley();
        InstallWater();
        FillNiche();
        FillNicheCabbage();

    }

    //////////////////Mountain landscape NEW////////////////////////////
    public void MountainsLandscape()
    {
        Clear();
        Clear();
        Clear();
        Clear();
        Clear();

        SetElevation_Elevation = 0;
        SetElevation();

        ManySteps_MaxStepHeight = 0.2f;
        ManySteps_NSteps = 20;
        ManySteps();


        ManySteps_MaxStepHeight = 0.1f;
        ManySteps_NSteps = 60;
        ManySteps();

        ManySteps_MaxStepHeight = 0.002f;
        ManySteps_NSteps = 1000;
        ManySteps();

        Smooth();
        Smooth();
        Smooth();

        InstallWater();
        FillNiche();
        FillNicheCoral();
        FillNicheSeaweed();

        TextureTerrain();
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

    //texture

    public void TextureTerrain()
    {
        Terrain thisTerrian = GetComponent<Terrain>();
        if (thisTerrian == null)
            throw new System.Exception("TerrainSquitch requires a Terrain. Add a terrain to " + gameObject.name);

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrian.terrainData.heightmapResolution;
        heightMapLength = thisTerrian.terrainData.heightmapResolution;
        Debug.Log(heightMapWidth + heightMapLength);

        int alphaMapSize;
        alphaMapSize = thisTerrian.terrainData.alphamapResolution;

        float[,] heights;
        heights = thisTerrian.terrainData.GetHeights(0, 0, heightMapWidth, heightMapLength);

        const int BOTTOM = 0;
        const int MID = 1;
        const int TOP = 2;
        const int POINT = 3;
        const int NTERRAINLAYERS = 4;

        float[,,] alphasAtMapPos = new float[alphaMapSize, alphaMapSize, NTERRAINLAYERS];

        Vector3 mapPos;
        mapPos.z = 0;
        for (mapPos.z = 0; mapPos.z < alphaMapSize; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < alphaMapSize; mapPos.x++)
            {
                if (mapPos.z < 100)
                {
                    alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, BOTTOM] = 0;
                    alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MID] = 1;
                    alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, TOP] = 0;
                    alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, POINT] = 0;
                }
                else if (mapPos.z < 200)
                {
                    if (heights[(int)mapPos.z, (int)mapPos.x] < 0.2)
                    {
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, BOTTOM] = 0.5f;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MID] = 0.5f;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, TOP] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, POINT] = 0;
                    }
                    else
                    {
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, BOTTOM] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MID] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, TOP] = 1;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, POINT] = 1;
                    }
                }
                else
                {
                    if (heights[(int)mapPos.z, (int)mapPos.x] < 0.2)
                    {
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, BOTTOM] = 1;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MID] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, TOP] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, POINT] = 0;
                    }
                    else
                    {
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, BOTTOM] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MID] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, TOP] = 1;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, POINT] = 1;
                    }
                }
            }
        }
        thisTerrian.terrainData.SetAlphamaps(0, 0, alphasAtMapPos);

    }

    public void TextureValley()
    {
        Terrain thisTerrian = GetComponent<Terrain>();
        if (thisTerrian == null)
            throw new System.Exception("TerrainSquitch requires a Terrain. Add a terrain to " + gameObject.name);

        int heightMapWidth, heightMapLength;
        heightMapWidth = thisTerrian.terrainData.heightmapResolution;
        heightMapLength = thisTerrian.terrainData.heightmapResolution;
        Debug.Log(heightMapWidth + heightMapLength);

        int alphaMapSize;
        alphaMapSize = thisTerrian.terrainData.alphamapResolution;

        float[,] heights;
        heights = thisTerrian.terrainData.GetHeights(0, 0, heightMapWidth, heightMapLength);

        const int SAND = 3;
        const int DIRT = 5;
        const int GRASS = 4;
        const int MOSS = 6;
        const int NTERRAINLAYERS = 7;

        float[,,] alphasAtMapPos = new float[alphaMapSize, alphaMapSize, NTERRAINLAYERS];

        Vector3 mapPos;
        mapPos.z = 0;
        for (mapPos.z = 0; mapPos.z < alphaMapSize; mapPos.z++)
        {
            for (mapPos.x = 0; mapPos.x < alphaMapSize; mapPos.x++)
            {
                if (mapPos.z < 100)
                {
                    alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, SAND] = 0;
                    alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, DIRT] = 1;
                    alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, GRASS] = 0;
                    alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MOSS] = 0;
                }
                else if (mapPos.z < 200)
                {
                    if (heights[(int)mapPos.z, (int)mapPos.x] < 0.2)
                    {
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, SAND] = 0.5f;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, DIRT] = 0.5f;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, GRASS] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MOSS] = 0;
                    }
                    else
                    {
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, SAND] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, DIRT] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, GRASS] = 1;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MOSS] = 1;
                    }
                }
                else
                {
                    if (heights[(int)mapPos.z, (int)mapPos.x] < 0.2)
                    {
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, SAND] = 1;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, DIRT] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, GRASS] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MOSS] = 0;
                    }
                    else
                    {
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, SAND] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, DIRT] = 0;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, GRASS] = 1;
                        alphasAtMapPos[(int)mapPos.z, (int)mapPos.x, MOSS] = 1;
                    }
                }
            }
        }
        thisTerrian.terrainData.SetAlphamaps(0, 0, alphasAtMapPos);

    }

}
