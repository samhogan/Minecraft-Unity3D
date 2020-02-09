using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterChunk : MonoBehaviour
{
    //chunk size
    public const int waterHeight = 28;

    //0 = air, 1 = land
    public int[,] locs = new int[16,16];


    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(0, waterHeight, 0);
    }



    public void SetLocs(BlockType[,,] blocks)
    {
        int y;

       

        for(int x = 0; x < 16; x++)
        {
            for(int z = 0; z < 16; z++)
            {
                locs[x, z] = 0;

                y = TerrainChunk.chunkHeight - 1;

                //find the ground
                while(y > 0 && blocks[x+1, y, z+1] == BlockType.Air)
                {
                    y--;
                
                }

                if(y+1 < waterHeight)
                    locs[x, z] = 1;
            }
        }
        
    }

    Vector2[] uvpat = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };

    public void BuildMesh()
    {
        Mesh mesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for(int x = 0; x < 16; x++)
            for(int z = 0; z < 16; z++)
            { 
                    if(locs[x,z]==1)
                    {
                        verts.Add(new Vector3(x, 0, z));
                        verts.Add(new Vector3(x, 0, z+1));
                        verts.Add(new Vector3(x+1, 0, z+1));
                        verts.Add(new Vector3(x+1, 0, z));

                        verts.Add(new Vector3(x, 0, z));
                        verts.Add(new Vector3(x, 0, z + 1));
                        verts.Add(new Vector3(x + 1, 0, z + 1));
                        verts.Add(new Vector3(x + 1, 0, z));


                        uvs.AddRange(uvpat);
                        uvs.AddRange(uvpat);
                        int tl = verts.Count-8;
                        tris.AddRange(new int[] { tl, tl + 1, tl + 2, tl, tl + 2, tl + 3,
                        tl+3+4,tl+2+4,tl+4,tl+2+4,tl+1+4,tl+4});
                    }
            }

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        
    }

}
