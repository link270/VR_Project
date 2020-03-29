using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public abstract class RotateStatue : MonoBehaviour
{
    public HoverButton hoverButton;

    public GameObject wall;
   
    private void Start()
    {
        hoverButton.onButtonDown.AddListener(OnButtonDown);
        //hoverButton.onButtonUp.AddListener(OnButtonUp);
    }

    private void OnButtonDown(Hand hand)
    {

        // System.Threading.Thread.Sleep(500);
        // t+=1;
        StartCoroutine(Rotate());
    }

    private void OnButtonUp(Hand hand)
    {
        //StartCoroutine(MoveWall());
    }

    public abstract IEnumerator Rotate();
    
    }
