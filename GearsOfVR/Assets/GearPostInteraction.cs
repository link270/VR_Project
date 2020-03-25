using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPostInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gearPresent;

    public float scale;

    public GameObject Gear;

    public float offsetTemp;

    void OnTriggerEnter(Collider colider){

        //var post = colider.attachedRigidbody.gameObject;
        var inventorLength = 0.76f;

        var offset = scale * inventorLength;
        offsetTemp = offset;
        Debug.Log(colider.gameObject.tag);
        
        if(colider.gameObject.tag == "GearPost" && this.gearPresent == false){
            var post = colider.gameObject;

            this.Gear.transform.position = new Vector3(post.transform.position.x, post.transform.position.y, post.transform.position.z + offset);
            this.Gear.transform.eulerAngles = Vector3.zero;
            this.Gear.GetComponent<Rigidbody>().useGravity = false;
            this.Gear.GetComponent<Rigidbody>().isKinematic = true;
            this.Gear.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.Gear.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            this.Gear.GetComponent<Gear>().IsRotating = true;
            this.gearPresent = true;
        }
    }

    void OnTriggerExit(Collider colider){

        if(colider.gameObject.tag == "GearPost"){
            this.gearPresent = false;
            this.Gear.GetComponent<Gear>().IsRotating = false;
            this.Gear.GetComponent<Rigidbody>().useGravity = true;
        }
            
    }
}
