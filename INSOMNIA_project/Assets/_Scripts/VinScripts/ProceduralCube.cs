using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralCube : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    public float scale = 1f;
    float adjScale;

    public int posX;
    public int posY;
    public int posZ;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Update()
    {
        adjScale = scale * .5f;
        MakeCube(adjScale, new Vector3(posX * scale, posY * scale, posZ * scale));
        UpdateMesh();
    }

    void MakeCube(float cubeScale, Vector3 cubePos)
    {
        vertices.Clear();
        triangles.Clear();


        for(int i = 0; i < 6; i++)
        {
            MakeFace(i, cubeScale, cubePos);
        }

    }

    void MakeFace(int direction, float faceScale, Vector3 facePos)
    {
        vertices.AddRange(CubeMeshData.faceVertices(direction, faceScale, facePos));
        int vCount = vertices.Count;

        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 1);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4);
        triangles.Add(vCount - 4 + 2);
        triangles.Add(vCount - 4 + 3);
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
}
