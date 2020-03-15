using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzleResult : MonoBehaviour
{
    public GameObject statueOneOne;
    public GameObject statueOneTwo;
    public GameObject statueTwoOne;
    public GameObject statueTwoTwo;
    public GameObject statueThreeOne;
    public GameObject statueThreeTwo;
    public GameObject exit;

    private Vector3 initPos;
    private Vector3 curPos;

    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = new Vector3(exit.transform.position.x,exit.transform.position.y,exit.transform.position.z);
        curPos = initPos;
        endPos = initPos;
        endPos.y += 2f;

    }

    // Update is called once per frame
    void Update()
    {
        var active11 = statueOneOne.GetComponent<Orientation>().active;
        var active12 = statueOneTwo.GetComponent<Orientation>().active;
        var active21 = statueTwoOne.GetComponent<Orientation>().active;
        var active22 = statueTwoTwo.GetComponent<Orientation>().active;
        var active31 = statueThreeOne.GetComponent<Orientation>().active;
        var active32 = statueThreeTwo.GetComponent<Orientation>().active;

        if(active11 && active12 && active21 && active22 && active31 && active32){
            //Debug.Log(removed1);
            Retract();
            //moved = true;
        } else Close();
    }

    void Retract(){
        StartCoroutine(MoveWall(endPos)); 
    }

    void Close(){
        StartCoroutine(MoveWall(initPos));
    }

    private IEnumerator MoveWall(Vector3 newPos){
        float startTime = Time.time;
        float overTime = 0.2f;
        float endTime = startTime + overTime;

        while (Time.time < endTime)
        {
            exit.transform.position = Vector3.Slerp(curPos, newPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        curPos = newPos;
    }
}

