using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class audioPlayerTiggered : MonoBehaviour
{
    private AudioSource message;

    private bool played;

    public float sensitivity;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        played = false;
        player = Player.instance;

        message = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(this.transform.position, player.transform.position);

//        Debug.Log(distance);

        if(!played && distance < sensitivity){
            played = true;
            message.Play();
        }
    }
}
