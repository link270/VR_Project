using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTileFall : MonoBehaviour
{

    private bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other){
        if(other.gameObject.layer == 8 && started == false){
            StartCoroutine(LowerTile());
            started = true;    
        }
    }

    IEnumerator LowerTile(){
        yield return new WaitForSeconds(1f);
        Vector3 curPos = gameObject.transform.position;
        Vector3 newPos = curPos;
        newPos.y += (-6f);
        float startTime = Time.time;
        float journeyTime = 1f;
        float endTime = startTime + journeyTime;

        while (Time.time < endTime){
            gameObject.transform.position =  Vector3.Slerp(curPos, newPos, (Time.time - startTime) / journeyTime);
            yield return null;
        }
    }
}
