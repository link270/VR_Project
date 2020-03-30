using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    public float orientation = 0;
    private float defaultOrientation;
    public float DefaultOrientation { get; private set; }
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        DefaultOrientation = orientation;
    }

    // Update is called once per frame
    void Update()
    {
        if(orientation % 360 == 0) isActive = true;
        else isActive = false;
    }
}
