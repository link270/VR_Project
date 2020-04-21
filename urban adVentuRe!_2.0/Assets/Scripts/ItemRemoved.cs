using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRemoved : MonoBehaviour
{
    public bool Removed;

    private AudioSource success;
    private Light spotLight;

    private void Start() {
        success = GetComponent<AudioSource>();
        spotLight = GetComponentInChildren<Light>();
    }

    void OnTriggerEnter(Collider colider){
        if(colider.tag == "IndianaStatues"){
            this.Removed = false;
            spotLight.intensity = 1.5f;
        }
    }

    void OnTriggerExit(Collider colider){
        if(colider.tag == "IndianaStatues"){
            this.Removed = true;
            success.Play();
            spotLight.intensity = 0.5f;
        }
    }
}
