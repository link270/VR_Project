using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class buttonMoveWall : MonoBehaviour
{
    public HoverButton hoverButton;

    public GameObject wall;
    private Vector3 initPos;
    private Vector3 curPos;

    private void Start()
    {
        hoverButton.onButtonDown.AddListener(OnButtonDown);
        hoverButton.onButtonUp.AddListener(OnButtonUp);
        initPos = wall.transform.position;
        curPos = initPos;
    }

    private void OnButtonDown(Hand hand)
    {
        Debug.Log("Wallhere");

        Vector3 newPos = curPos;
        // System.Threading.Thread.Sleep(500);
        // t+=1;
        newPos.y += 1.0f;
        StartCoroutine(MoveWall(newPos));
    }

    private void OnButtonUp(Hand hand)
    {
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
