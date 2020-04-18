using UnityEngine;

public class BowScore : MonoBehaviour
{
    private int currentScore;
    private bool hasWon;
    private AudioSource WinSound;

    public int targetScore = 3;

    // Start is called before the first frame update
    private void Start()
    {
        currentScore = 0;
        hasWon = false;
        WinSound = GetComponent<AudioSource>();
    }

    private bool CheckForWin()
    {
        return currentScore >= targetScore;

    }

    public void incrementScore()
    {
        currentScore++;
        if (CheckForWin() && !hasWon)
        {
            Debug.Log("You hit enought targets and win.");
            hasWon = true;
            WinSound.Play();
        }
    }
}
