using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GearsMoveWall : MonoBehaviour
{
     public List<GameObject> GearPosts;
    public GameObject Door;
    private Vector3 initPos;
    private Vector3 curPos;

    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = new Vector3(Door.transform.position.x, Door.transform.position.y, Door.transform.position.z);
        curPos = initPos;
        endPos = initPos;
        endPos.y -= 5f;

    }

    // Update is called once per frame
    void Update()
    {
        if(GearPosts.All(Post =>  Post.GetComponent<GearPostInteraction1>().CorrectGearPresent)){
           Retract();
        }
    }

    void Retract(){
        StartCoroutine(MoveWall(endPos)); 
    }

    private IEnumerator MoveWall(Vector3 newPos){
        float startTime = Time.time;
        float overTime = 2f;
        float endTime = startTime + overTime;

        while (Time.time < endTime)
        {
            Door.transform.position = Vector3.Slerp(curPos, newPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        curPos = newPos;
    }
}
