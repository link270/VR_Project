using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RotateStatueCW : RotateStatue
{

    public override IEnumerator Rotate(){

        float startTime = Time.time;
        float overTime = 0.2f;
        float endTime = startTime + overTime;

        float totalRotation = 180f;
        float rotationProgress = 0;
        float perLoop = 3.0f;

        Orientation statueOrientation = statue.GetComponent<Orientation>();
        while (rotationProgress < totalRotation)
        {
            statue.transform.Rotate(0.0f, perLoop, 0.0f, Space.Self);
            rotationProgress += perLoop;
            statueOrientation.orientation += perLoop;
            if(statueOrientation.orientation == 360) statueOrientation.orientation = 0;
            yield return null;
        }

    }
}
