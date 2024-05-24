using JetBrains.Annotations;
using Palmmedia.ReportGenerator.Core;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace
{
    NoiseGenerator noiseGen;
    Mesh mesh;
    PlanetSettings settings;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    List<Vector3> Vertices = new List<Vector3>();
    List<int> Triangles = new List<int>();

    public TerrainFace(NoiseGenerator noiseGenerator ,Mesh mesh, PlanetSettings settings, Vector3 localUp)
    {
        this.mesh = mesh;
        this.settings = settings;
        this.localUp = localUp;
        this.noiseGen = noiseGenerator;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] Verts = new Vector3[settings.Resolution * settings.Resolution];
        int[] tris = new int[(settings.Resolution - 1) * (settings.Resolution - 1) * 6];
        int triIndex = 0;
        for (int y = 0; y < settings.Resolution; y++)
        {
            for (int x = 0; x < settings.Resolution; x++)
            {
                int i = x + y * settings.Resolution;
                Vector2 percent = new Vector2(x, y) / (settings.Resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                Verts[i] = noiseGen.CalculatePointOnPlanet(pointOnUnitSphere); //calculate point on planet

                if(x != settings.Resolution - 1 && y != settings.Resolution - 1)
                {
                    tris[triIndex] = i;
                    tris[triIndex + 1] = i+ settings.Resolution + 1;
                    tris[triIndex + 2] = i+ settings.Resolution;

                    tris[triIndex + 3] = i;
                    tris[triIndex + 4] = i + 1;
                    tris[triIndex + 5] = i + settings.Resolution + 1;
                    triIndex += 6;
                }
            }
        }

        mesh.Clear();
        mesh.vertices = Verts;
        mesh.triangles = tris;
        mesh.RecalculateBounds();
    }

    public void ConstructTree(PlanetSettings settings)
    {
        Vertices.Clear();
        Triangles.Clear();

        Chunk parentChunk = new Chunk(null, null, localUp.normalized * PlanetaryGeneration.size, settings.PlanetRadius, 0, localUp, axisA, axisB);
        parentChunk.GenerateChildren();

        int triangleOffset = 0;
        foreach(Chunk child in parentChunk.GetVisibleChildren())
        {
            //(Vector3[], int[]) VertsAndTris = child
        }
    }
}
