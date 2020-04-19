using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    private AudioSource hitSound;

    private BowScore scoreKeeper;
    private bool hasBeenHit;

    public void Start()
    {
        hitSound = GetComponent<AudioSource>();
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<BowScore>();
        hasBeenHit = false;
    }

    public void HitTarget()
    {
        if (!hasBeenHit)
        {
            hasBeenHit = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            hitSound.Play();
            //StartCoroutine(PlayHitSound());
            //gameObject.SetActive(false);
            scoreKeeper.incrementScore(gameObject.tag);
            Destroy(gameObject, hitSound.clip.length);
        }
    }

    //private IEnumerator PlayHitSound()
    //{
    //    hitSound.Play();
    //    yield return new WaitWhile(() => hitSound.isPlaying);
    //}

    public IEnumerator MoveTargets()
    {
        var curPos = gameObject.transform.position;
        var newPos = curPos;
        float overTime = 3f;


        while (gameObject.activeSelf == true)
        {
            float startTime = Time.time;
            float endTime = startTime + overTime;
            if (curPos.y < 4f) newPos.y = curPos.y + 3.0f;
            else newPos.y = curPos.y - 3.0f;

            while (Time.time < endTime)
            {
                gameObject.transform.position = Vector3.Slerp(curPos, newPos, (Time.time - startTime) / overTime);
                yield return null;
        }

            curPos = newPos;
        }

        Debug.Log("Ended moving");
    }
}
