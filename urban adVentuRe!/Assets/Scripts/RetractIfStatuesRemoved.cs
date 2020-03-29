using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RetractIfStatuesRemoved : MonoBehaviour
{
    public GameObject statueOne;
    public GameObject statueTwo;
    public GameObject statueThree;
    public GameObject statueFour;
    public GameObject ballDetector;
    public GameObject trapdoor;

    public GameObject Ball;

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

        var removed = new List<bool>(){
            statueOne.GetComponent<ItemRemoved>().Removed,
            statueTwo.GetComponent<ItemRemoved>().Removed,
            statueThree.GetComponent<ItemRemoved>().Removed,
            statueFour.GetComponent<ItemRemoved>().Removed
        };


        var ballDetected = ballDetector.GetComponent<DetectBall>().BallPresent;
        if(removed.All(statue => statue == true)){
            //Debug.Log(removed1);
            Retract();
            //moved = true;
        }

        if(ballDetected)
        {
            Close();
        }

        if(removed.Where(statue => statue == true).ToList().Count == 1){
            Ball.GetComponent<Rigidbody>().useGravity = true;
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
