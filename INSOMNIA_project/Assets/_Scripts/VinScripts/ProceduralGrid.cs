using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public float cellSize = 1f;
    public Vector3 gridOffset;
    public int gridSize = 4;
    public int gridSizeX = 4;
    public int gridSizeY = 4;
    public int gridSizeZ = 4;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;        
    }

    // Start is called before the first frame update
    private void Update()
    {
        //MakeDiscreteProceduralGrid();
        MakeDiscreteProcedural3DGrid();
        //MakeContinuousProceduralGrid();
        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void MakeDiscreteProceduralGrid()
    {
        //set array sizes
        vertices = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];

        //set tracker integers
        int v = 0;
        int t = 0;

        //set vertex offset
        float vertexOffset = cellSize * .5f;

        for(int x = 0; x < gridSize; x++)
        {
            for(int y = 0; y < gridSize; y++)
            {
                Vector3 cellOffset = new Vector3(x * cellSize * 1.5f, 0f, y * cellSize * 1.5f);

                //populate the vertices and triangle arrays
                vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                vertices[v + 1] = new Vector3(-vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                vertices[v + 2] = new Vector3(vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                vertices[v + 3] = new Vector3(vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;

                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;


            }

        }
    }

    void MakeContinuousProceduralGrid()
    {
        //set array sizes
        vertices = new Vector3[(gridSize + 1) * (gridSize + 1)];
        triangles = new int[gridSize * gridSize * 6];

        //set tracker integers
        int v = 0;
        int t = 0;

        //set vertex offset
        float vertexOffset = cellSize * .5f;


        //create vertex grid
        for (int x = 0; x <= gridSize; x++)
        {
            for (int y = 0; y <= gridSize; y++)
            {
                vertices[v] = new Vector3((x * cellSize) - vertexOffset, 0f, (y * cellSize) - vertexOffset);
                v++;
            }

        }

        //reset vertex tracker
        v = 0;

        //setting each cell's triangles
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                triangles[t] = v;
                triangles[t + 1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + (gridSize + 1);
                triangles[t + 5] = v + (gridSize + 1) + 1;
                v++;
                t += 6;
            }
            v++;

        }
    }

    void MakeDiscreteProcedural3DGrid()
    {
        //set array sizes
        vertices = new Vector3[gridSizeX * gridSizeY * gridSizeZ * 8];
        triangles = new int[gridSizeX * gridSizeY * gridSizeZ * 36];

        //set tracker integers
        int v = 0;
        int t = 0;

        //set vertex offset
        float vertexOffset = cellSize * .5f;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for(int z = 0; z < gridSizeZ; z++)
                {
                    Vector3 cellOffset = new Vector3(x * cellSize * 1.5f, y * cellSize * 1.5f, z * cellSize * 1.5f);

                    //populate the vertices and triangle arrays
                    vertices[v] = new Vector3(-vertexOffset, -vertexOffset, -vertexOffset) + cellOffset + gridOffset;
                    vertices[v + 1] = new Vector3(-vertexOffset, -vertexOffset, vertexOffset) + cellOffset + gridOffset;
                    vertices[v + 2] = new Vector3(vertexOffset, -vertexOffset, -vertexOffset) + cellOffset + gridOffset;
                    vertices[v + 3] = new Vector3(vertexOffset, -vertexOffset, vertexOffset) + cellOffset + gridOffset;
                    vertices[v + 4] = new Vector3(-vertexOffset, vertexOffset, -vertexOffset) + cellOffset + gridOffset;
                    vertices[v + 5] = new Vector3(-vertexOffset, vertexOffset, vertexOffset) + cellOffset + gridOffset;
                    vertices[v + 6] = new Vector3(vertexOffset, vertexOffset, -vertexOffset) + cellOffset + gridOffset;
                    vertices[v + 7] = new Vector3(vertexOffset, vertexOffset, vertexOffset) + cellOffset + gridOffset;

                    triangles[t] = triangles[t + 6] = triangles[t + 32] = v;
                    triangles[t + 1] = triangles[t + 4] = triangles[t + 8] = triangles[t + 9] = triangles[t + 18] = v + 2;
                    triangles[t + 2] = triangles[t + 3] = triangles[t + 26] = triangles[t + 30] = triangles[t + 33] = v + 1;
                    triangles[t + 5] = triangles[t + 20] = triangles[t + 21] = triangles[t + 29] = triangles[t + 24] = v + 3;
                    triangles[t + 7] = triangles[t + 10] = triangles[t + 12] = triangles[t + 31] = triangles[t + 35] = v + 4;
                    triangles[t + 11] = triangles[t + 14] = triangles[t + 15] = triangles[t + 19] = triangles[t + 22] = v + 6;
                    triangles[t + 13] = triangles[t + 16] = triangles[t + 25] = triangles[t + 28] = triangles[t + 34] = v + 5;
                    triangles[t + 17] = triangles[t + 23] = triangles[t + 27] = v + 7;

                    v += 8;
                    t += 36;
                }
            }
        }
    }
}
