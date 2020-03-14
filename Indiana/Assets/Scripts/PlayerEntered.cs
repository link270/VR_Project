using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntered : MonoBehaviour
{
    public GameObject Ball;
    
    void OnTriggerEnter (Collider other) 
    {
        Debug.Log("Wallhere");
        Ball.GetComponent<Rigidbody>().useGravity = true;
    }
}
