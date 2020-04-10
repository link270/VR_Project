using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 2000.0f;
    public Transform tip = null;

    private AudioSource impactSound;
    private Rigidbody arrowRigidbody = null;
    private bool isStopped = true;

    private Vector3 lastPos = Vector3.zero;

    private void Awake()
    {
        arrowRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        lastPos = transform.position;
        impactSound = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (isStopped)
        {
            return;
        }

        arrowRigidbody.MoveRotation(Quaternion.LookRotation(arrowRigidbody.velocity, transform.up));

        RaycastHit hit;
        if(Physics.Linecast(lastPos, tip.position, out hit))
        {
            Stop(hit.collider.gameObject);
        }

        lastPos = tip.position;
    }

    private void Stop(GameObject hitObject)
    {
        isStopped = true;
        transform.parent = hitObject.transform;
        arrowRigidbody.isKinematic = true;
        arrowRigidbody.useGravity = false;
        check(hitObject);
        impactSound.Play();
    }

    public void discard()
    {
        Destroy(gameObject, 0.0f);
    }

    private void check(GameObject hitObject)
    {
        MonoBehaviour[] behaviours = hitObject.GetComponents<MonoBehaviour>();

        foreach(MonoBehaviour behaviour in behaviours)
        {
            if(behaviour is IDamageable)
            {
                IDamageable damageable = (IDamageable)behaviour;
                damageable.Damage(5);
                break;
            }
        }
    }

    public void Fire(float pullValue)
    {
        lastPos = transform.position;
        isStopped = false;
        transform.parent = null;
        arrowRigidbody.isKinematic = false;
        arrowRigidbody.useGravity = true;
        arrowRigidbody.AddForce(transform.forward * (pullValue * speed));
    }
}
