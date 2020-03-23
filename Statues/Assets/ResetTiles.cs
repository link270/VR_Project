using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Valve.VR.InteractionSystem
{
public class ResetTiles : MonoBehaviour
{
    public HoverButton lever;
    
    private  Player player;
    private GameObject vrcamera;
    private GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        //vrcamera = GameObject.Find("VRCamera");
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    public void OnLeverPressed(Hand hand){
        StartCoroutine(ResetTilePuzzle());
    }

    private IEnumerator ResetTilePuzzle(){
        //Debug.Log(player.transform.position);
        // GameObject PlayerCamera = GameObject.Find("VRCamera");  //get the VRcamera object
        // Debug.Log("Camera: " + PlayerCamera.name);
        // Debug.Log("Player: " + player.name);
        // Vector3 GlobalCameraPosition = PlayerCamera.transform.position;  //get the global position of VRcamera
        // Vector3 GlobalPlayerPosition = player.transform.position;
        // Vector3 GlobalOffsetCameraPlayer = new Vector3(GlobalCameraPosition.x - GlobalPlayerPosition.x, 0, GlobalCameraPosition.z - GlobalPlayerPosition.z);
        // Vector3 newRigPosition = new Vector3(gameObject.transform.position.x - GlobalOffsetCameraPlayer.x, player.transform.position.y, gameObject.transform.position.z - GlobalOffsetCameraPlayer.z);
        // player.transform.position = newRigPosition;
            SteamVR_Fade.Start(Color.black, 0.2f);
            yield return new WaitForSeconds(0.2f);
            //player.transform.Rotate(Vector3.up, 90f);
            Vector3 translation = gameObject.transform.position - player.transform.position;
            player.transform.Translate(translation.x, translation.y, translation.z, Space.World);
            SteamVR_Fade.Start(Color.clear, 0.2f);
            yield return new WaitForSeconds(0.2f);
            //vrcamera.transform.position = new Vector3(1,1,1);
        //Debug.Log(gameObject.transform.position);
        
        foreach (GameObject tile in tiles){
            //Debug.Log("Resetting " + tile.name);
            tile.transform.position = new Vector3(tile.transform.position.x, 0f, tile.transform.position.z);
        }
        yield return null;
    }

}
}