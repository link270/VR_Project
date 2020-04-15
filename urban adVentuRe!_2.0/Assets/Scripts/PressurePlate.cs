using UnityEngine;

public class PressurePlate : MonoBehaviour
 {
     
     public float endstop;//how far down you want the button to be pressed before it triggers
     public bool Pressed;

     private Transform Location;
     private Vector3 StartPos;
     // Start is called before the first frame update
     void Start()
     {
         Location = GetComponent<Transform>();
         StartPos = Location.position;
     }
     void Update()
     {
         
        if (Location.position.y - StartPos.y< endstop) {//check to see if the button has been pressed all the way down

            Location.position= new Vector3(Location.position.x,endstop+StartPos.y,Location.position.z);
            GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
            Pressed = true;//update pressed
        }

        if (Location.position.y> StartPos.y) {
            Location.position= new Vector3(Location.position.x,StartPos.y,Location.position.z);
            GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezeAll;
        }
     
     }
     void OnCollisionExit(Collision collision)//check for when to unlock the button
     {
         Debug.Log("unlock");
         if (collision.gameObject.tag == "Sphere") {
             GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY; //Remove Y movement constraint.
             Pressed = false;//update pressed
         }
     }
 }