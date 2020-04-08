using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Post : MonoBehaviour
{
    public int PostNum;

    public bool Available;

    public int rotationDirection;

    private void Start() {
        Available = true;
    }
}
