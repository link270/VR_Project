using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRemoved : MonoBehaviour
{
    public bool Removed;

    void OnTriggerEnter(Collider colider){
        this.Removed = false;
    }

    void OnTriggerExit(Collider colider){
        this.Removed = true;
    }
}
