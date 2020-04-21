using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using System.Linq;


public class ResetStatues : MonoBehaviour
{
    public HoverButton hoverButton;
    private GameObject [] statues;
    private List<Orientation> orientations;

    private AudioSource Hint;
    private bool playedHint;
    // Start is called before the first frame update
    void Start()
    {
        playedHint = false;
        Hint = GetComponent<AudioSource>();
        statues = GameObject.FindGameObjectsWithTag("Statues");
        orientations = new List<Orientation>();
        foreach(GameObject statue in statues){
            orientations.Add(statue.GetComponent<Orientation>());
        }

        hoverButton.onButtonDown.AddListener(OnButtonDown);
    }
    private void OnButtonDown(Hand hand)
    {
        if(!playedHint){
            playedHint = true;
            Hint.Play();
        }
        StartCoroutine(Rotate());
    }
    
    public IEnumerator Rotate(){
        for(int i = 0; i < statues.Length; ++i){
            if(orientations.ElementAt(i).orientation != orientations.ElementAt(i).DefaultOrientation){
                float startTime = Time.time;
                float overTime = 0.2f;
                float endTime = startTime + overTime;

                float totalRotation = 180f;
                float rotationProgress = 0;
                float perLoop = 3.0f;

                while (rotationProgress < totalRotation)
                {
                    statues[i].transform.Rotate(0.0f, perLoop, 0.0f, Space.Self);
                    rotationProgress += perLoop;
                    orientations.ElementAt(i).orientation += perLoop;
                    if(orientations.ElementAt(i).orientation == 360) orientations.ElementAt(i).orientation = 0;
                    yield return null;
                }
            }
        }
    }
}
