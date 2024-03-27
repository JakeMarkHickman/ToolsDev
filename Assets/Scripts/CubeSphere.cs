using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSphere : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] Tris;


    public bool GenerateCubeSphere(float Resolution)
    {
        if(!CreateVerts(Resolution)) { return false; }
        mesh.vertices = vertices;

        if (!CreateTris()) { return false; }
        mesh.vertices = Tris;

        return true;
    }

    private bool CreateVerts(float Resolution)
    {
        return false;
    }

    private bool CreateTris()
    {
        return false;
    }
}
