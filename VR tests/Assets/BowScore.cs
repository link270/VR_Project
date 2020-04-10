using UnityEngine;

public class BowScore : MonoBehaviour
{
    private int currentScore;
    private bool hasWon;
    private AudioSource solvedSound;

    public int targetScore = 5;

    // Start is called before the first frame update
    private void Start()
    {
        currentScore = 0;
        hasWon = false;
        solvedSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (CheckForWin() && !hasWon)
        {
            solvedSound.Play();
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
