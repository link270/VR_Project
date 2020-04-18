using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    private AudioSource impactSound;

    private BowScore scoreKeeper;
    private bool hasBeenHit;

    public void Start()
    {
        impactSound = GetComponent<AudioSource>();
        scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<BowScore>();
        hasBeenHit = false;
    }

    public void Damage(int amount)
    {
        if(!hasBeenHit) targetHit();
    }

    private void targetHit()
    {
        hasBeenHit = true;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.black;
        Debug.Log("Target hit");
        impactSound.Play();
        scoreKeeper.incrementScore();
    }
}
