using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chunk
{
    Chunk[] Children;
    Chunk Parent;

    Vector3 Position;

    float radius;

    int DetailLevel;

    Vector3 localUp;

    Vector3 axisA;
    Vector3 axisB;

    NoiseGenerator noiseGen;

    PlanetSettings Settings;


    public Chunk(Chunk[] ChildrenChunks, Chunk ParentChunk, Vector3 pos, float radius, int LOD, Vector3 localUp, Vector3 axisA, Vector3 axisB,PlanetSettings settings ,NoiseGenerator noiseGenerator)
    {
        this.Children = ChildrenChunks;
        this.Parent = ParentChunk;
        this.Position = pos;
        this.radius = radius;
        this.DetailLevel = LOD;
        this.localUp = localUp;
        this.axisA = axisA;
        this.axisB = axisB;
        this.Settings = settings;
        this.noiseGen = noiseGenerator;
    }

    public void GenerateChildren()
    {
        if (DetailLevel <= 8 && DetailLevel >= 0)
        {
            if(Vector3.Distance(Position.normalized * Settings.PlanetRadius, PlanetaryGeneration.Camera.position /*+ noiseGen.CalculatePointOnPlanet(PlanetaryGeneration.Camera.position)*/ / (Settings.PlanetRadius)) <= PlanetaryGeneration.LOD[DetailLevel] / (Settings.PlanetRadius / 2))
            {
                Children = new Chunk[4];
                Children[0] = new Chunk(new Chunk[0], this, Position + axisA * radius / 2 + axisB * radius / 2, radius / 2, DetailLevel + 1, localUp, axisA, axisB, Settings, noiseGen);
                Children[1] = new Chunk(new Chunk[0], this, Position + axisA * radius / 2 - axisB * radius / 2, radius / 2, DetailLevel + 1, localUp, axisA, axisB, Settings, noiseGen);
                Children[2] = new Chunk(new Chunk[0], this, Position - axisA * radius / 2 + axisB * radius / 2, radius / 2, DetailLevel + 1, localUp, axisA, axisB, Settings, noiseGen);
                Children[3] = new Chunk(new Chunk[0], this, Position - axisA * radius / 2 - axisB * radius / 2, radius / 2, DetailLevel + 1, localUp, axisA, axisB, Settings, noiseGen);


                foreach (Chunk child in Children)
                {
                    child.GenerateChildren();
                }
            }
        }
    }

    public Chunk[] GetVisibleChildren()
    {
        List<Chunk> toBeRendered = new List<Chunk>();

        if(Children.Length > 0)
        {
            foreach (Chunk child in Children)
            {
                toBeRendered.AddRange(child.GetVisibleChildren());
            }
        }
        else
        {
            toBeRendered.Add(this);
        }

        return toBeRendered.ToArray();
    }

    public (Vector3[], int[]) CalculateVertsAndTris(int triangleOffset, PlanetSettings settings)
    {
        Vector3[] verts = new Vector3[settings.Resolution * settings.Resolution];
        int[] tris = new int[(settings.Resolution - 1) * (settings.Resolution - 1) * 6];
        int triIndex = 0;

        for(int y = 0; y < settings.Resolution; y++)
        {
            for(int x  = 0; x < settings.Resolution; x++)
            {
                int i = x + y * settings.Resolution;
                Vector2 percent = new Vector2(x, y) / (settings.Resolution - 1);

                Vector3 pointOnUnitCube = Position + ((percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB) * radius;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized * Settings.PlanetRadius;
                verts[i] = noiseGen.CalculatePointOnPlanet(pointOnUnitSphere); //calculate point on planet

                if (x != settings.Resolution - 1 && y != settings.Resolution - 1)
                {
                    tris[triIndex] = i + triangleOffset;
                    tris[triIndex + 1] = i + settings.Resolution + 1 + triangleOffset;
                    tris[triIndex + 2] = i + settings.Resolution + triangleOffset;

                    tris[triIndex + 3] = i + triangleOffset;
                    tris[triIndex + 4] = i + 1 + triangleOffset;
                    tris[triIndex + 5] = i + settings.Resolution + 1 + triangleOffset;
                    triIndex += 6;
                }
            }
        }

        return (verts, tris);
    }


}
