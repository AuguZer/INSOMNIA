using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VoxelRender : MonoBehaviour
{
    Mesh mesh;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();

    public float scale = 1f;
    float adjScale;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjScale = scale * .5f;
    }

    private void Start()
    {
        GenerateVoxelMesh(new VoxelData());
        UpdateMesh();
    }

    void GenerateVoxelMesh(VoxelData voxelData)
    {
        vertices.Clear();
        triangles.Clear();

        for (int z = 0; z < voxelData.Depth; z++)
        {
            for(int x = 0; x < voxelData.Width; x++)
            {
                if(voxelData.GetCell(x, z) == 0)
                {
                    continue;
                }
                
                MakeCube(adjScale, new Vector3(x * scale, 0, z * scale));
            }
        }
    }

    void MakeCube(float cubeScale, Vector3 cubePos)
    {
        for (int i = 0; i < 6; i++)
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
