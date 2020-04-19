using System.Collections;
using UnityEngine;

public class BowScore : MonoBehaviour
{
    private int currentScore;
    private bool hasWon;
    private AudioSource winSound;
    private int targetScore = 2;
    private GameObject[] fenceTargets;
    private GameObject[] doorTargets;

    public GameObject door;

    // Start is called before the first frame update
    private void Start()
    {
        currentScore = 0;
        hasWon = false;
        winSound = GetComponent<AudioSource>();
        fenceTargets = GameObject.FindGameObjectsWithTag("FenceTargets");
        doorTargets = GameObject.FindGameObjectsWithTag("DoorTargets");

        foreach (var target in fenceTargets) target.SetActive(false);
        foreach (var target in doorTargets) target.SetActive(false);
    }

    // Track the current score vs the current target score. 
    private bool CheckForWin()
    {
        return currentScore >= targetScore;
    }

    public void incrementScore(string tag)
    {
        currentScore++;
        if (CheckForWin() && !hasWon)
        {
            switch (tag)
            {
                case "WallTargets":
                    winSound.Play();
                    foreach (var target in fenceTargets)
                    {
                        target.SetActive(true);
                        StartCoroutine(target.GetComponent<Target>().MoveTargets());
                    }

                    currentScore = 0;
                    targetScore = 5;
                    break;

                case "FenceTargets":
                    winSound.Play();
                    foreach (var target in doorTargets)
                    {
                        target.SetActive(true);
                    }

                    currentScore = 0;
                    targetScore = 3;
                    break;

                case "DoorTargets":
                    hasWon = true;
                    winSound.Play();
                    StartCoroutine(OpenDoor());
                    break;
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        float startTime = Time.time;
        float overTime = 2f;
        float endTime = startTime + overTime;
        var curPos = door.transform.position;
        var newPos = curPos;
        newPos.x += 4f;
        door.GetComponent<AudioSource>().Play();

        while (Time.time < endTime)
        {
            door.transform.position = Vector3.Slerp(curPos, newPos, (Time.time - startTime) / overTime);
            yield return null;
        }
    }
}
