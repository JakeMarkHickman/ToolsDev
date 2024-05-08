using UnityEngine;

public class TerrainFace : MonoBehaviour
{
    NoiseGenerator noiseGen;
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(NoiseGenerator noiseGenerator ,Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;
        this.noiseGen = noiseGenerator;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] Verts = new Vector3[resolution * resolution];
        int[] tris = new int[(resolution - 1) * (resolution - 1) * 6];
        int triIndex = 0;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                Verts[i] = noiseGen.CalculatePointOnPlanet(pointOnUnitSphere); //calculate point on planet

                if(x != resolution - 1 && y != resolution - 1)
                {
                    tris[triIndex] = i;
                    tris[triIndex + 1] = i+resolution+1;
                    tris[triIndex + 2] = i+resolution;

                    tris[triIndex + 3] = i;
                    tris[triIndex + 4] = i + 1;
                    tris[triIndex + 5] = i + resolution + 1;
                    triIndex += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = Verts;
        mesh.triangles = tris;
        mesh.RecalculateBounds();
    }
}
