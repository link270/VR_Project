using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class Gear : MonoBehaviour
{
    // Start is called before the first frame update
    public float RPM;

    public GameObject gear;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Rotate());
    }


    public IEnumerator Rotate(){

        float startTime = Time.time;
        float overTime = 0.2f;
        float endTime = startTime + overTime;

        float rotationProgress = 0;
        float perLoop = RPM * .25f;

        gear.transform.Rotate(0.0f, 0.0f, perLoop, Space.Self);
        rotationProgress += perLoop;

        yield return null;
    }
}
