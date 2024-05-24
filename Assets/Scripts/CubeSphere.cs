using UnityEngine;

public class CubeSphere
{

    NoiseGenerator noiseGenerator;

    [SerializeField, HideInInspector]
    MeshFilter[] MeshFilters;
    TerrainFace[] terrainFaces;

    public void Init(GameObject parent, PlanetSettings settings)
    {
        CubeSetUp(parent, settings);
        GenerateMesh(settings);
    }

    void CubeSetUp(GameObject parent, PlanetSettings settings)
    {
        noiseGenerator = new NoiseGenerator(settings);
        if (MeshFilters == null || MeshFilters.Length == 0)
        {
            MeshFilters = new MeshFilter[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] Directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (MeshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = parent.transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                MeshFilters[i] = meshObj.AddComponent<MeshFilter>();
                MeshFilters[i].sharedMesh = new Mesh();
            }

            terrainFaces[i] = new TerrainFace(noiseGenerator, MeshFilters[i].sharedMesh, settings, Directions[i]);
        }
    }

    void GenerateMesh(PlanetSettings settings)
    {
        foreach (TerrainFace face in terrainFaces)
        {
            if(settings.DynamicPlanet)
            {
                face.ConstructTree(settings);
            }
            else
            {
                face.ConstructMesh();
            }
        }
    }
}