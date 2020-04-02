using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling_Ball : MonoBehaviour
{
    //public Rigidbody ball;
    public GameObject trapDoor;
    public GameObject ball;

    public int visibleDistance;

    public int angularVelocity;
    // Start is called before the first frame update
    void Start()
    {
        ball.GetComponent<Rigidbody>().maxAngularVelocity = angularVelocity;
        ball.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(ball.transform.position, trapDoor.transform.position) < visibleDistance){
            ball.GetComponent<MeshRenderer>().enabled = true;
        }else{
            ball.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
