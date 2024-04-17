using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryGeneration : MonoBehaviour
{
    public int PlanetSeed;

    public float PlanetRadius;
    public float PlanetMass;

    [Range(2, 256)]
    public int Resolution;

    [SerializeField, HideInInspector]
    MeshFilter[] MeshFilters;
    TerrainFace[] terrainFaces;

    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }

    void Initialize()
    {
        if (MeshFilters == null || MeshFilters.Length == 0)
        {
            MeshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] Directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for(int i = 0; i < 6; i++)
        {
            if (MeshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                MeshFilters[i] = meshObj.AddComponent<MeshFilter>();
                MeshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFace(MeshFilters[i].sharedMesh, Resolution, Directions[i]);
        }
    }

    void GenerateMesh()
    {
        foreach(TerrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }
}
