using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChunk : MonoBehaviour
{
    //chunk size
    public const int chunkWidth = 16;
    public const int chunkHeight = 64;

    //0 = air, 1 = land
    public int[,,] blocks = new int[chunkWidth+2, chunkHeight, chunkWidth+2];


    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void BuildMesh()
    {
        Mesh mesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        for(int x = 1; x < chunkWidth+1; x++)
            for(int z = 1; z < chunkWidth+1; z++)
                for(int y = 0; y < chunkHeight; y++)
                {
                    if(blocks[x, y, z] == 1)
                    {
                        Vector3 blockPos = new Vector3(x-1, y, z-1);
                        int numFaces = 0;
                        //no land above, build top face
                        if(y < chunkHeight - 1 && blocks[x, y + 1, z] == 0)
                        {
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 0));
                            numFaces++;
                        }

                        //bottom
                        if(y > 0 && blocks[x, y - 1, z] == 0)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            numFaces++;
                        }

                        //front
                        if(blocks[x, y, z-1] == 0)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 0, 0));
                            numFaces++;
                        }

                        //right
                        if(blocks[x+1, y, z] == 0)
                        {
                            verts.Add(blockPos + new Vector3(1, 0, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 0));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            numFaces++;
                        }

                        //back
                        if(blocks[x, y, z + 1] == 0)
                        {
                            verts.Add(blockPos + new Vector3(1, 0, 1));
                            verts.Add(blockPos + new Vector3(1, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            numFaces++;
                        }

                        //left
                        if(blocks[x - 1, y, z] == 0)
                        {
                            verts.Add(blockPos + new Vector3(0, 0, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 1));
                            verts.Add(blockPos + new Vector3(0, 1, 0));
                            verts.Add(blockPos + new Vector3(0, 0, 0));
                            numFaces++;
                        }


                        int tl = verts.Count - 4*numFaces;
                        for(int i = 0; i < numFaces; i++)
                        {
                            tris.AddRange(new int[] { tl+i*4, tl + i * 4 + 1, tl + i * 4 + 2, tl + i * 4, tl + i * 4 + 2, tl + i * 4 + 3 });
                        }
                    }
                }

        print(verts);
        print(tris);

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();

        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }


    void AddSquare(List<Vector3> verts, List<int> tris)
    {

    }
}
