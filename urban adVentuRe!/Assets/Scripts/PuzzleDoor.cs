using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    private Vector3 initPos;
    private Vector3 curPos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);
        curPos = initPos;
        endPos = initPos;
        endPos.y += 2f;
    }

    public IEnumerator RaiseDoor(){
        float startTime = Time.time;
        float overTime = 0.2f;
        float endTime = startTime + overTime;

        while (Time.time < endTime)
        {
            gameObject.transform.position = Vector3.Slerp(curPos, endPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        curPos = endPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
