using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    int[] matCounts = new int[] { 0, 0, 0, 0 };

    public BlockType[] matTypes;
    public Image[] invImgs;
    public Image[] matImgs;

    int curMat;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Image img in matImgs)
        {
            img.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            SetCur(0);
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            SetCur(1);
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            SetCur(2);
        else if(Input.GetKeyDown(KeyCode.Alpha4))
            SetCur(3);
    }

    void SetCur(int i)
    {
        invImgs[curMat].color = new Color(0, 0, 0, 43/255f);

        curMat = i;
        invImgs[i].color = new Color(0, 0, 0, 80/255f);
    }

    public bool CanPlaceCur()
    {
        return matCounts[curMat] > 0;
    }

    public BlockType GetCurBlock()
    {
        return matTypes[curMat];
    }

    public void ReduceCur()
    {
        matCounts[curMat]--;

        if(matCounts[curMat] == 0)
            matImgs[curMat].gameObject.SetActive(false);
    }

    public void AddToInventory(BlockType block)
    {
        int i = 0;
        if(block == BlockType.Stone)
            i = 1;
        else if(block == BlockType.Trunk)
            i = 2;
        else if(block == BlockType.Leaves)
            i = 3;

        matCounts[i]++;
        if(matCounts[i] == 1)
            matImgs[i].gameObject.SetActive(true);

    }
}
