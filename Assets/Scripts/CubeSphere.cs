using UnityEngine;

public class CubeSphere : MonoBehaviour
{
    [SerializeField, HideInInspector]
    MeshFilter[] MeshFilters;
    TerrainFace[] terrainFaces;

    public void Init(int resolution, GameObject parent, PlanetSettings settings)
    {
        CubeSetUp(resolution, parent);
        GenerateMesh(settings);
    }

    void CubeSetUp(int resolution, GameObject parent)
    {
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

            terrainFaces[i] = new TerrainFace(MeshFilters[i].sharedMesh, resolution, Directions[i]);
        }
    }

    void GenerateMesh(PlanetSettings settings)
    {
        foreach (TerrainFace face in terrainFaces)
        {
            face.ConstructMesh(settings);
        }
    }
}