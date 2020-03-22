using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ResetTiles : MonoBehaviour
{
    public HoverButton lever;
    
    private Player player;
    private GameObject vrcamera;
    private GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        vrcamera = GameObject.Find("VRCamera");
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        lever.onButtonDown.AddListener(OnbuttonDown);
    }

    private void OnbuttonDown(Hand hand){
        StartCoroutine(ResetTilePuzzle());
    }

    private IEnumerator ResetTilePuzzle(){
        Debug.Log(player.transform.position);
        player.transform.position = gameObject.transform.position;
        vrcamera.transform.position = gameObject.transform.position;
        Debug.Log(gameObject.transform.position);
        
        foreach (GameObject tile in tiles){
            //Debug.Log("Resetting " + tile.name);
            tile.transform.position = new Vector3(tile.transform.position.x, 0f, tile.transform.position.z);
        }
        return null;
    }

}
