using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public GameObject door;

    void OnTriggerEnter(Collider colider){
        door.transform.position += new Vector3(0, 4, 0);
    }
}
