using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class StatuePuzzleResult : MonoBehaviour
{

    private GameObject [] statues;
    private List<Orientation> orientations;
    public GameObject wall;
    public TeleportArea teleportArea;
    public AudioSource sound;

    private float initPos;
    private float curPos;
    private float endPos;
    private bool isOpen;
    private bool isClosed;

    // Start is called before the first frame update
    void Start()
    {
        statues = GameObject.FindGameObjectsWithTag("Statues");
        orientations = new List<Orientation>();
        foreach(GameObject statue in statues){
            orientations.Add(statue.GetComponent<Orientation>());
        }
        initPos = 0f;
        curPos = initPos;
        endPos = initPos + 90f;

        isClosed = true;
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldUpdate = true;
        foreach(Orientation statue in orientations){
            shouldUpdate = statue.isActive && shouldUpdate;
        }


        if(shouldUpdate && !isOpen){
            //Debug.Log("Hi");

            isOpen = true;
            Retract();
            //moved = true;
        } else if (!shouldUpdate && !isClosed){
            //Debug.Log("Ho");
            isClosed = true;
            Close();
        } 
    }

    void Retract(){
        StartCoroutine(OpenDoor()); 
    }

    void Close(){
        StartCoroutine(CloseDoor());
    }

    private IEnumerator OpenDoor(){
        sound.Play();
        teleportArea.SetLocked(false);
        float startTime = Time.time;
        float duration = 1f;
        float endTime = startTime + duration;

        float angle = -1*(120 - curPos);

        while (Time.time < endTime){
            wall.transform.RotateAround(gameObject.transform.position, Vector3.up, angle * Time.deltaTime);
            curPos += angle * Time.deltaTime;
            yield return null;
        }
        isClosed = false;
    }

    private IEnumerator CloseDoor(){
        sound.Play();
        teleportArea.SetLocked(true);
        float startTime = Time.time;
        float duration = 1f;
        float endTime = startTime + duration;

        float angle = (0-curPos);

        while (Time.time < endTime){
            wall.transform.RotateAround(gameObject.transform.position, Vector3.up, angle * Time.deltaTime);
            curPos += angle * Time.deltaTime;
            yield return null;
        }
        isOpen = false;
    }

    private IEnumerator MoveWall(float newPos){
        float startTime = Time.time;
        float duration = 2f;
        float endTime = startTime + duration;

        float angle;
        
        while (Time.time < endTime)
        {
            
            angle = (curPos - newPos) * (Time.time - startTime) / duration;

            wall.transform.RotateAround(gameObject.transform.position, Vector3.up, angle);
            curPos += angle;
            yield return null;
        }

        curPos = newPos;
    }
}

