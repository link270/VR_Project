using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PatternTile : MonoBehaviour
{
    private HoverButton hoverButton;
    // Start is called before the first frame update
    void Start()
    {
        hoverButton = gameObject.GetComponent<HoverButton>();
        if(hoverButton){
            hoverButton.onButtonDown.AddListener(this.OnButtonDown);
        }

    }

    private void OnButtonDown(Hand hand){
        Debug.Log("***Pressed " + gameObject.name);
        SendMessageUpwards("TileWasActivated", gameObject.name);
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player"){
            Debug.Log("Player entered");
        }
        SendMessageUpwards("TileWasActivated", gameObject.name);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
