using System.Collections;
using System.Collections.Generic;
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


    public Chunk(Chunk[] ChildrenChunks, Chunk ParentChunk, Vector3 pos, float radius, int LOD, Vector3 localUp, Vector3 axisA, Vector3 axisB)
    {
        this.Children = ChildrenChunks;
        this.Parent = ParentChunk;
        this.Position = pos;
        this.radius = radius;
        this.DetailLevel = LOD;
        this.localUp = localUp;
        this.axisA = axisA;
        this.axisB = axisB;
    }

    public void GenerateChildren()
    {
        if (DetailLevel <= 8 && DetailLevel >= 0)
        {
            if(Vector3.Distance(Position.normalized * PlanetaryGeneration.size, PlanetaryGeneration.Camera.position) <= PlanetaryGeneration.LOD[DetailLevel])
            {
                Children = new Chunk[4];
                Children[0] = new Chunk(new Chunk[0], this, Position + axisA * radius / 2 + axisB * radius / 2, radius / 2, DetailLevel + 1, localUp, axisA, axisB);
                Children[1] = new Chunk(new Chunk[0], this, Position + axisA * radius / 2 - axisB * radius / 2, radius / 2, DetailLevel + 1, localUp, axisA, axisB);
                Children[3] = new Chunk(new Chunk[0], this, Position - axisA * radius / 2 + axisB * radius / 2, radius / 2, DetailLevel + 1, localUp, axisA, axisB);
                Children[4] = new Chunk(new Chunk[0], this, Position - axisA * radius / 2 - axisB * radius / 2, radius / 2, DetailLevel + 1, localUp, axisA, axisB);


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


        return (verts, tris);
    }


}
