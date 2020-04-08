using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Arrow")]
    public GameObject arrowPrefab = null;
    public int totalArrows = 5;

    [Header("Bow")]
    public float grabThreashold = 0.15f;
    public Transform start = null;
    public Transform end = null;
    public Transform socket = null;

    private Transform pullingHand = null;
    private Arrow currentArrow = null;
    private Arrow newArrow = null;
    private Animator animator = null;
    private Queue<Arrow> activeArrows;
    private bool fired = true;

    private float pullValue = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        activeArrows = new Queue<Arrow>();
        //StartCoroutine(CreateArrow(0.0f));
    }

    private void Update()
    {
        if(!pullingHand || !newArrow)
        {
            return;
        }

        pullValue = CalculatePull(pullingHand);

        pullValue = Mathf.Clamp(pullValue, 0.0f, 1.0f);

        animator.SetFloat("Blend", pullValue);
    }

    private float CalculatePull(Transform pullHand)
    {
        Vector3 direction = end.position - start.position;
        float magnitude = direction.magnitude;

        direction.Normalize();
        Vector3 difference = pullHand.position - start.position;

        return Vector3.Dot(difference, direction) / magnitude;
    }

    private IEnumerator CreateArrow(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject arrowObject = Instantiate(arrowPrefab, socket);

        arrowObject.transform.localPosition = new Vector3(0, 0, 0.425f);
        arrowObject.transform.localEulerAngles = Vector3.zero;

        newArrow = arrowObject.GetComponent<Arrow>();
    }

    public void Pull(Transform hand)
    {


        if (fired)
        {
            StartCoroutine(CreateArrow(0.0f));
            fired = false;
        }
        float distance = Vector3.Distance(hand.position, start.position);

        if (distance > grabThreashold)
        {
            return;
        }

        pullingHand = hand;
    }

    public void Release()
    {
        if(pullValue > 0.20f)
        {
            FireArrow();
        }

        pullingHand = null;

        pullValue = 0.0f;
        animator.SetFloat("Blend", 0);

        //if (!currentArrow)
        //{
        //    StartCoroutine(CreateArrow(0.0f));
        //}
    }

    private void FireArrow()
    {
        fired = true;
        //newArrow = null;
        newArrow.Fire(pullValue);
        activeArrows.Enqueue(newArrow);
        if (activeArrows.Count > totalArrows)
        {
            activeArrows.Dequeue().discard();
        }
    }
}
