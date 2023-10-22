using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralMesh : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeMeshData();
        CreateMesh();
    }

    void MakeMeshData()
    {
        vertices = new Vector3[] { new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 1f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 1f) };

        triangles = new int[] { 0, 1, 2, 1, 3, 2 };

        //Cube Data
        //Vector3 v0 = new Vector3(0f, 0f, 0f);
        //Vector3 v1 = new Vector3(0f, 0f, 1f);
        //Vector3 v2 = new Vector3(1f, 0f, 0f);
        //Vector3 v3 = new Vector3(1f, 0f, 1f);
        //Vector3 v4 = new Vector3(1f, 1f, 1f);
        //Vector3 v5 = new Vector3(0f, 1f, 1f);
        //Vector3 v6 = new Vector3(0f, 1f, 0f);
        //Vector3 v7 = new Vector3(1f, 1f, 0f);



        //vertices = new Vector3[] {v0, v1, v2, v3, v4, v5, v6, v7};

        //triangles = new int[] { 0, 2, 1, 1, 2, 3, 2, 7, 4, 2, 4, 3, 0, 6, 2, 2, 6, 7, 7, 6, 5, 7, 5, 4, 3, 4, 5, 3, 5, 1, 1, 5, 0, 6, 0, 5};

    }

    void CreateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
