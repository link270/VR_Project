using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRemoved : MonoBehaviour
{
    public bool Removed;

    private AudioSource success;

    private void Start() {
        success = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider colider){
        if(colider.tag == "IndianaStatues"){
            this.Removed = false;
        }
    }

    void OnTriggerExit(Collider colider){
        if(colider.tag == "IndianaStatues"){
            this.Removed = true;
            success.Play();
        }
    }
}
