using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject terrainChunk;

    public Transform player;

    Dictionary<ChunkPos, TerrainChunk> chunks = new Dictionary<ChunkPos, TerrainChunk>();

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        LoadChunks();
    }



    void BuildChunk(int xPos, int zPos)
    {
        GameObject chunkGO = Instantiate(terrainChunk, new Vector3(xPos, 0, zPos), Quaternion.identity);
        TerrainChunk chunk = chunkGO.GetComponent<TerrainChunk>();

        for(int x = 0; x < TerrainChunk.chunkWidth+2; x++)
            for(int z = 0; z < TerrainChunk.chunkWidth+2; z++)
                for(int y = 0; y < TerrainChunk.chunkHeight; y++)
                {
                    if(Mathf.PerlinNoise((xPos + x-1) * .1f, (zPos + z-1) * .1f) * 10 + y < TerrainChunk.chunkHeight * .5f)
                        chunk.blocks[x, y, z] = 1;
                }

        chunk.BuildMesh();

        chunks.Add(new ChunkPos(xPos, zPos), chunk);
    }


    int chunkDist = 3;
    ChunkPos curChunk = new ChunkPos(-1,-1);
    void LoadChunks()
    {
        //the current chunk the player is in
        int curChunkPosX = Mathf.FloorToInt(player.position.x/16)*16;
        int curChunkPosZ = Mathf.FloorToInt(player.position.z/16)*16;

        //entered a new chunk
        if(curChunk.x != curChunkPosX || curChunk.z != curChunkPosZ)
        {
            curChunk.x = curChunkPosX;
            curChunk.z = curChunkPosZ;


            for(int i = curChunkPosX - 16 * chunkDist; i <= curChunkPosX + 16 * chunkDist; i += 16)
                for(int j = curChunkPosZ - 16 * chunkDist; j <= curChunkPosZ + 16 * chunkDist; j += 16)
                {
                    ChunkPos cp = new ChunkPos(i, j);

                    if(!chunks.ContainsKey(cp))
                        BuildChunk(i, j);

                }

            //remove chunks that are too far away
            List<ChunkPos> toDestroy = new List<ChunkPos>();
            //unload chunks
            foreach(KeyValuePair<ChunkPos, TerrainChunk> c in chunks)
            {
                ChunkPos cp = c.Key;
                if(Mathf.Abs(curChunkPosX - cp.x) > 16 * (chunkDist + 1) || 
                    Mathf.Abs(curChunkPosZ - cp.z) > 16 * (chunkDist + 1))
                {
                    toDestroy.Add(c.Key);
                }
            }

            foreach(ChunkPos cp in toDestroy)
            {
                Destroy(chunks[cp].gameObject);
                chunks.Remove(cp);
            }

        }





    }

}


public struct ChunkPos
{
    public int x, z;
    public ChunkPos(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
}