using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PatternTile : MonoBehaviour
{
    private HoverButton hoverButton;
    public Color color;
    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        hoverButton = gameObject.GetComponent<HoverButton>();
        if(hoverButton){
            hoverButton.onButtonDown.AddListener(this.OnButtonDown);
        }
    }

    private void OnButtonDown(Hand hand){
        SendMessageUpwards("TileWasActivated", gameObject.name);
    }

}
