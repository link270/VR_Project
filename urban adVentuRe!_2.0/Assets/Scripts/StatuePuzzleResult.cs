using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuePuzzleResult : MonoBehaviour
{

    private GameObject [] statues;
    private List<Orientation> orientations;
    public GameObject wall;

    private Vector3 initPos;
    private Vector3 curPos;

    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        statues = GameObject.FindGameObjectsWithTag("Statues");
        orientations = new List<Orientation>();
        foreach(GameObject statue in statues){
            orientations.Add(statue.GetComponent<Orientation>());
        }
        initPos = new Vector3(wall.transform.position.x,wall.transform.position.y,wall.transform.position.z);
        curPos = initPos;
        endPos = initPos;
        endPos.y += 2f;

    }

    // Update is called once per frame
    void Update()
    {
        bool shouldUpdate = true;
        foreach(Orientation statue in orientations){
            shouldUpdate = statue.isActive && shouldUpdate;
        }

        if(shouldUpdate){
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
            wall.transform.position = Vector3.Slerp(curPos, newPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        curPos = newPos;
    }
}

