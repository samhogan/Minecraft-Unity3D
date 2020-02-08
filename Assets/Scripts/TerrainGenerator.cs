using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        print(Mathf.PerlinNoise(0, 0));
        print(Mathf.PerlinNoise(1, 1));
        print(Mathf.PerlinNoise(2, 2));
        print(Mathf.PerlinNoise(.1f,.1f));

        for(int x = -100; x <= 100; x++)
            for(int z = -100; z <= 100; z++)
            {
                float y = Mathf.PerlinNoise(x*.06f, z*.06f)*20;
                Instantiate(cube, new Vector3(x, (int)y, z), Quaternion.identity);
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
