using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToiletPaper : MonoBehaviour
{
    private bool Removed;
    private AudioSource success;
    private GameObject[] credits;
    private Text thanks;

    private void Start() {
        Removed = false;
        success = GetComponent<AudioSource>();
        credits = GameObject.FindGameObjectsWithTag("Credits");
        thanks = GameObject.FindGameObjectWithTag("Thanks").GetComponent<Text>();
    }

    void OnTriggerExit(Collider colider){
        if(colider.tag == "ToiletPaper" && !Removed)
        {
            Removed = true;
            success.Play();
            StartCoroutine(Display(credits));
        }
    }

    IEnumerator Display(GameObject[] credits)
    {
        foreach (var c in credits)
        {
            c.GetComponent<Text>().enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(DisplayFinal(credits));
    }

    IEnumerator DisplayFinal(GameObject[] credits)
    {
        yield return new WaitForSeconds(5f);
        foreach (var c in credits)
        {
            c.GetComponent<Text>().enabled = false;
        }
        yield return new WaitForSeconds(1f);
        thanks.enabled = true;
        yield return new WaitForSeconds(5f);
        thanks.enabled = false;
    }
}
