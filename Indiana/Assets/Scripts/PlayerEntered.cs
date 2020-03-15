using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntered : MonoBehaviour
{
    public GameObject Ball;
    
    void OnTriggerEnter (Collider other) 
    {
        Debug.Log("Detected a thing");
        Ball.GetComponent<Rigidbody>().useGravity = true;
    }
}
