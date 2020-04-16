using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initPos;
    private Vector3 curPos;
    public GameObject spikes;
    private Vector3 endPos;

    private Player player;  

    void Start()
    {
        player = Player.instance;
        initPos = new Vector3(spikes.transform.position.x, spikes.transform.position.y, spikes.transform.position.z);
        curPos = initPos;
        endPos = initPos;
        endPos.y += .5f;
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(spikes.transform.position, player.transform.position);

//        Debug.Log(distance);

        if(distance < 1.5){
            Activate();
        }

        if(distance > 1.5){
            Retract();
        }
    }

    void Activate(){
        StartCoroutine(ActivateSpikes(endPos)); 
    }

    void Retract(){
        StartCoroutine(ActivateSpikes(initPos));
    }

    private IEnumerator ActivateSpikes(Vector3 newPos){
        float startTime = Time.time;
        float overTime = 2f;
        float endTime = startTime + overTime;

        while (Time.time < endTime)
        {
            spikes.transform.position = Vector3.Slerp(curPos, newPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        curPos = newPos;
    }
}
