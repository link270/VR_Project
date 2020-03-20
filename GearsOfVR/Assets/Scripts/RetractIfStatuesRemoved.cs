using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractIfStatuesRemoved : MonoBehaviour
{
    public GameObject statueOne;
    public GameObject statueTwo;
    public GameObject statueThree;
    public GameObject statueFour;
    public GameObject ballDetector;
    public GameObject trapdoor;

    private Vector3 initPos;
    private Vector3 curPos;

    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = new Vector3(trapdoor.transform.position.x,trapdoor.transform.position.y,trapdoor.transform.position.z);
        curPos = initPos;
        endPos = initPos;
        endPos.z += 5f;

    }

    // Update is called once per frame
    void Update()
    {
        var removed1 = statueOne.GetComponent<ItemRemoved>().Removed;
        var removed2 = statueTwo.GetComponent<ItemRemoved>().Removed;
        var removed3 = statueThree.GetComponent<ItemRemoved>().Removed;
        var removed4 = statueFour.GetComponent<ItemRemoved>().Removed;

        var ballDetected = ballDetector.GetComponent<DetectBall>().BallPresent;
        if(removed1 && removed2 && removed3 && removed4){
            //Debug.Log(removed1);
            Retract();
            //moved = true;
        }

        if(ballDetected)
        {
            Close();
        }
    }

    void Retract(){
        StartCoroutine(MoveWall(endPos)); 
    }

    void Close(){
        StartCoroutine(MoveWall(initPos));
    }

    private IEnumerator MoveWall(Vector3 newPos){
        float startTime = Time.time;
        float overTime = 2f;
        float endTime = startTime + overTime;

        while (Time.time < endTime)
        {
            trapdoor.transform.position = Vector3.Slerp(curPos, newPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        curPos = newPos;
    }
}
