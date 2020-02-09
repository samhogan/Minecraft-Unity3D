using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainModifier : MonoBehaviour
{
    public LayerMask groundLayer;

    public Inventory inv;

    float maxDist = 4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool leftClick = Input.GetMouseButtonDown(0);
        bool rightClick = Input.GetMouseButtonDown(1);
        if(leftClick || rightClick)
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDist, groundLayer))
            {
                Vector3 pointInTargetBlock;

                //destroy
                if(rightClick)
                    pointInTargetBlock = hitInfo.point + transform.forward * .01f;//move a little inside the block
                else
                    pointInTargetBlock = hitInfo.point - transform.forward * .01f;

                //get the terrain chunk (can't just use collider)
                int chunkPosX = Mathf.FloorToInt(pointInTargetBlock.x / 16f) * 16;
                int chunkPosZ = Mathf.FloorToInt(pointInTargetBlock.z / 16f) * 16;

                ChunkPos cp = new ChunkPos(chunkPosX, chunkPosZ);

                TerrainChunk tc = TerrainGenerator.chunks[cp];

                //index of the target block
                int bix = Mathf.FloorToInt(pointInTargetBlock.x) - chunkPosX+1;
                int biy = Mathf.FloorToInt(pointInTargetBlock.y);
                int biz = Mathf.FloorToInt(pointInTargetBlock.z) - chunkPosZ+1;

                if(rightClick)//replace block with air
                {
                    inv.AddToInventory(tc.blocks[bix, biy, biz]);
                    tc.blocks[bix, biy, biz] = BlockType.Air;
                    tc.BuildMesh();
                }
                else if(leftClick)
                {
                    if(inv.CanPlaceCur())
                    {
                        tc.blocks[bix, biy, biz] = inv.GetCurBlock();

                        tc.BuildMesh();

                        inv.ReduceCur();
                    }
                    
                }
            }
        }
    }
}
