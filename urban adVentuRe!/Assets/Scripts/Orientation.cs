using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    public float orientation = 0;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(orientation % 180 == 0) isActive = true;
        else isActive = false;
    }
}
