using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 2000.0f;
    public Transform tip = null;

    private Rigidbody arrowRigidbody = null;
    private bool isStopped = true;

    private Vector3 lastPos = Vector3.zero;

    private void Awake()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isStopped)
        {
            return;
        }

        arrowRigidbody.MoveRotation(Quaternion.LookRotation(arrowRigidbody.velocity, transform.up));

        if(Physics.Linecast(lastPos, tip.position))
        {
            Stop();
        }

        lastPos = tip.position;
    }

    private void Stop()
    {
        isStopped = true;
        arrowRigidbody.isKinematic = true;
        arrowRigidbody.useGravity = false;
    }

    public void discard()
    {
        Destroy(gameObject, 0.0f);
    }

    public void Fire(float pullValue)
    {
        isStopped = false;
        transform.parent = null;
        arrowRigidbody.isKinematic = false;
        arrowRigidbody.useGravity = true;
        arrowRigidbody.AddForce(transform.forward * (pullValue * speed));
    }
}
