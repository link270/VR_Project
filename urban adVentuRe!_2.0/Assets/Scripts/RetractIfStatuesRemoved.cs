using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RetractIfStatuesRemoved : MonoBehaviour
{

    public GameObject ballDetector;
    public GameObject trapdoor;
    public GameObject[] pedestals;
    private List<ItemRemoved> removed;
    public GameObject Ball;
    private DetectBall detectBall;
    private Vector3 initPos;
    private Vector3 curPos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        pedestals = GameObject.FindGameObjectsWithTag("IndianaPedestals");
        removed = new List<ItemRemoved>();
        foreach(GameObject pedestal in pedestals){
            removed.Add(pedestal.GetComponent<ItemRemoved>());
        }
        initPos = new Vector3(trapdoor.transform.position.x,trapdoor.transform.position.y,trapdoor.transform.position.z);
        curPos = initPos;
        endPos = initPos;
        endPos.z += 5f;

        detectBall = ballDetector.GetComponent<DetectBall>();

    }

    // Update is called once per frame
    void Update()
    {

        bool allRemoved = removed.All(pedestal => pedestal.Removed == true);
        
        if(allRemoved){
            Retract();
        }

        if(detectBall.BallPresent)
        {
            Close();
        }

        if(removed.Where(pedestal => pedestal.Removed == true).ToList().Count == 1){
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
