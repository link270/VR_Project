using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RotateStatueCW : RotateStatue
{

    public override IEnumerator MoveWall(){

        float startTime = Time.time;
        float overTime = 0.2f;
        float endTime = startTime + overTime;

        float totalRotation = 90f;
        float rotationProgress = 0;
        float perLoop = 3.0f;

        while (rotationProgress < totalRotation)
        {
            wall.transform.Rotate(0.0f, perLoop, 0.0f, Space.Self);
            rotationProgress += perLoop;
            wall.GetComponent<Orientation>().orientation += perLoop;
            if(wall.GetComponent<Orientation>().orientation == 180) wall.GetComponent<Orientation>().orientation = 0;
            yield return null;
        }

    }
}
