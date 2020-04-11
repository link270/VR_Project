using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    public int PostNum;

    public bool Available;

    public int rotationDirection;

    public GameObject Gear;

    private void Start() {
        Available = true;
    }
}
