using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBall : MonoBehaviour
{
    public bool BallPresent;

    void OnTriggerEnter(Collider colider){
            this.BallPresent = true;

    }

    void OnTriggerExit(Collider colider){
            this.BallPresent = false;
    }
}
