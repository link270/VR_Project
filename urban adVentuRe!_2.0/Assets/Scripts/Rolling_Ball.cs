using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Rolling_Ball : MonoBehaviour
{
    //public Rigidbody ball;
    public GameObject trapDoor;
    public GameObject ball;
    public int visibleDistance;
    public int angularVelocity;
    private MeshRenderer ballMesh;
    public GameObject restore;

    // Start is called before the first frame update
    void Start()
    {
        ball.GetComponent<Rigidbody>().maxAngularVelocity = angularVelocity;
        ballMesh = ball.GetComponent<MeshRenderer>();
        ballMesh.enabled = false;
    }
    void OnCollisionEnter(Collision collider){
        Debug.Log(collider.gameObject.name + ", " + collider.gameObject.tag);
        if(collider.gameObject.tag == "Player"){
            Debug.Log("***BLARG, you died!");
            StartCoroutine(restore.GetComponent<IndianaReset>().ResetIndiana());
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(ball.transform.position, trapDoor.transform.position) < visibleDistance){
            ballMesh.enabled = true;
        }else{
            ballMesh.enabled = false;
        }
    }
}
