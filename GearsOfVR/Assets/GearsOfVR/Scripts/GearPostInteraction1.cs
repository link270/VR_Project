using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GearPostInteraction1 : MonoBehaviour
{
    public bool GearPresent;
    public bool CorrectGearPresent;
    public bool powered;
    public GameObject AdjacentGearPost;

    public List<GameObject> FollowingGearPosts;
    public List<GameObject> PossibleGears;

    public GameObject Post;

    public int CorrectTeethNumber;
    public float AttatchSensitivity; 

    public float DetatchSensitivity;

    public bool powerInput;

    public float Scale;

    // Start is called before the first frame update
    void Start()
    {
        if(powerInput)
        {
            powered = true;
        }else{
            powered = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(AdjacentGearPost.GetComponent<GearPostInteraction1>().powered && CorrectGearPresent || powerInput)
        {
            powered = true;
        }
        else
        {
            powered = false;
        }

        foreach(var gear in PossibleGears)
        {

            if((powered && CorrectGearPresent && gear.GetComponent<Gear>().IsPlaced && AdjacentGearPost.GetComponent<GearPostInteraction1>().CorrectGearPresent && gear.GetComponent<Gear>().PlacedOn == Post.GetComponent<Post>().PostNum))
            {
                Debug.Log("Powered: " + powered.ToString() + " Post: " + Post.GetComponent<Post>().PostNum.ToString() +  " Correct Gear: " + CorrectGearPresent.ToString() + " Gear: " + gear.GetComponent<Gear>().numberOfTeeth.ToString() + " Placed: " + gear.GetComponent<Gear>().IsPlaced + " Adjacent Post: " + AdjacentGearPost.GetComponent<Post>().PostNum.ToString() + " Adjacent correct: "  + AdjacentGearPost.GetComponent<GearPostInteraction1>().CorrectGearPresent.ToString());
                gear.GetComponent<Gear>().IsRotating = true;
            }

            if(!gear.GetComponent<Gear>().IsPlaced && Post.GetComponent<Post>().Available  && Vector3.Distance(gear.transform.position, Post.transform.position) < AttatchSensitivity)
            {
                // set rotate direction
                gear.GetComponent<Gear>().rotationDirection = Post.GetComponent<Post>().rotationDirection;

                AttachGear(gear);
                Post.GetComponent<Post>().Gear = gear;
                Post.GetComponent<Post>().Available = false;
                GearPresent = true;

                gear.GetComponent<Gear>().PlacedOn = Post.GetComponent<Post>().PostNum;

                if(gear.GetComponent<Gear>().numberOfTeeth == CorrectTeethNumber)
                {
                    Debug.Log(gear.GetComponent<Gear>().numberOfTeeth);


                    CorrectGearPresent = true;
                }


                gear.GetComponent<Gear>().IsPlaced = true;

            }

            if(gear.GetComponent<Gear>().IsPlaced && !Post.GetComponent<Post>().Available &&  gear.GetComponent<Gear>().PlacedOn == Post.GetComponent<Post>().PostNum  && Vector3.Distance(gear.transform.position, Post.transform.position) > DetatchSensitivity)
            {
                DettachGear(gear);
                Post.GetComponent<Post>().Available = true;
                CorrectGearPresent = false;
                GearPresent = false;

                foreach(var following in FollowingGearPosts){
                    if(!following.GetComponent<Post>().Available){
                        following.GetComponent<Post>().Gear.GetComponent<Gear>().IsRotating = false;
                    }
                }


                gear.GetComponent<Gear>().IsPlaced = false;

            }
        }
    }

    void AttachGear(GameObject gear)
    {
        var inventorLength = 0.76f;
 
        var offset = Scale * inventorLength;
        gear.GetComponent<SimpleAttach>().myHand.DetachObject(gear); // detachs hand 
        gear.GetComponent<Rigidbody>().useGravity = false; // turns off gravity

        gear.transform.position = new Vector3(Post.transform.position.x, Post.transform.position.y, Post.transform.position.z + offset); // moves gear into correct position
        gear.transform.rotation = new Quaternion(Post.transform.rotation.x, Post.transform.rotation.y, Post.transform.rotation.z, Post.transform.rotation.w); // ensure same rotation
        gear.transform.eulerAngles = Vector3.zero; // stops residual movement
        gear.GetComponent<Rigidbody>().isKinematic = true; 
        gear.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gear.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    
    void DettachGear(GameObject gear)
    {
        gear.GetComponent<SimpleAttach>().myHand.DetachObject(gear); // detachs hand 
        gear.GetComponent<Rigidbody>().useGravity = true; // turns off gravity
        gear.GetComponent<Rigidbody>().isKinematic = false;
        gear.GetComponent<Gear>().IsRotating = false;
    }
}
