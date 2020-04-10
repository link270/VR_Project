using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScore : MonoBehaviour
{
    private int currentScore;
    private bool hasWon;

    public int targetScore = 3;

    // Start is called before the first frame update
    private void Start()
    {
        currentScore = 0;
        hasWon = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (CheckForWin() && !hasWon)
        {
            Debug.Log("You hit enought targets and win.");
            hasWon = true;
        }
    }

    private bool CheckForWin()
    {
        return currentScore >= targetScore;

    }

    public void incrementScore()
    {
            currentScore++;
    }
}
