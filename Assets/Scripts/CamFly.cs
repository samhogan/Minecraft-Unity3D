using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFly : MonoBehaviour
{

    public float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }
}
