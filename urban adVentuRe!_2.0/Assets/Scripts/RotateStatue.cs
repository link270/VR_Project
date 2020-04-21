using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public abstract class RotateStatue : MonoBehaviour
{
    public HoverButton hoverButton;

    public GameObject statue;

    private AudioSource Hint;

    private GameObject HintObject;
   
    private void Start()
    {
        HintObject = GameObject.FindGameObjectWithTag("ButtonPressed");
        Hint = HintObject.GetComponent<AudioSource>();

        hoverButton.onButtonDown.AddListener(OnButtonDown);
        //hoverButton.onButtonUp.AddListener(OnButtonUp);
    }

    private void OnButtonDown(Hand hand)
    {
        if(!HintObject.GetComponent<StatueButtonPressed>().buttonPressed){
            HintObject.GetComponent<StatueButtonPressed>().buttonPressed = true;
            Hint.Play();
        }
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
