using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR.InteractionSystem;

public class GearPostInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gearPresent;

    public float scale;

    public int correctGearNumTeeth;
    public GameObject Gear;

    public List<GameObject> possibleGearPosts;
    public List<GameObject> adjacentGearPosts;

    public float offsetTemp;

    public float attatchSensitivity;
    public float dettatchSensitivity;

    void Update(){

        var inventorLength = 0.76f;

        var offset = scale * inventorLength;
        foreach(var post in possibleGearPosts){
            Debug.Log(Vector3.Distance(post.transform.position, Gear.transform.position));
            Debug.Log(gearPresent);
            if(!gearPresent && Vector3.Distance(post.transform.position, Gear.transform.position) < attatchSensitivity){
                
                Debug.Log(gearPresent);
                Gear.GetComponent<SimpleAttach>().myHand.DetachObject(Gear); // detachs hand 
                Gear.GetComponent<Rigidbody>().useGravity = false; // turns off gravity

                Gear.transform.position = new Vector3(post.transform.position.x, post.transform.position.y, post.transform.position.z + offset); // moves gear into correct position
                Gear.transform.rotation = new Quaternion(post.transform.rotation.x, post.transform.rotation.y, post.transform.rotation.z, post.transform.rotation.w); // ensure same rotation
                Gear.transform.eulerAngles = Vector3.zero; // stops residual movement
                Gear.GetComponent<Rigidbody>().isKinematic = true; 
                Gear.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Gear.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Gear.GetComponent<Gear>().IsRotating = true;
                gearPresent = true;
            }

            if(gearPresent && Vector3.Distance(post.transform.position, Gear.transform.position) > dettatchSensitivity){
                
                Debug.Log(gearPresent);
                Gear.GetComponent<SimpleAttach>().myHand.DetachObject(Gear); // detachs hand 
                Gear.GetComponent<Rigidbody>().useGravity = true; // turns off gravity
                Gear.GetComponent<Rigidbody>().isKinematic = false;
                Gear.GetComponent<Gear>().IsRotating = false;
                gearPresent = false;
            }
        }
    }
}