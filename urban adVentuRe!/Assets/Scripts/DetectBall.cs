using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBall : MonoBehaviour
{
    public bool BallPresent;

    void OnTriggerEnter(Collider colider){ 
            Debug.Log(colider.tag);

            if(colider.tag == "IndianaBall"){
                this.BallPresent = true;
            }       
    }
}
