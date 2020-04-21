using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class PuzzleDoor : MonoBehaviour
{
    private float initPos;
    private float curPos;
    private float endPos;
    public GameObject door;
    public TeleportArea teleportArea;

    // Start is called before the first frame update
    void Start()
    {
        initPos = 0;
        curPos = initPos;
        endPos = initPos;
        endPos += 2f;
    }
    public IEnumerator RaiseDoor(){
        gameObject.GetComponent<AudioSource>().Play();
        teleportArea.SetLocked(false);
        float startTime = Time.time;
        float duration = 1f;
        float endTime = startTime + duration;

        float angle = -1*(90 - curPos);

        while (Time.time < endTime){
            door.transform.RotateAround(gameObject.transform.position, Vector3.up, angle * Time.deltaTime);
            curPos += angle * Time.deltaTime;
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
