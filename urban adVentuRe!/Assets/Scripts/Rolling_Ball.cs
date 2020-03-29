using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling_Ball : MonoBehaviour
{
    public Rigidbody ball;
    public int angularVelocity;
    // Start is called before the first frame update
    void Start()
    {
        ball.maxAngularVelocity = angularVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
