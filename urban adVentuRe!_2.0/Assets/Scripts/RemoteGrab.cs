using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;
using Valve.VR;

public class RemoteGrab : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;

    private Hand hand;
    private GameObject attachedObj;
    private bool isAttached;

    //public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic;
    public Transform pointer;
    public LayerMask Grabable;
    public LayerMask BowGrabable;
    public float maxGrabbingDistance = 15f;
    private BlankScript blankScript;

    void Start()
    {
        hand = GetComponent<Hand>();
        isAttached = false;
        attachedObj = null;
        laserPointer = GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
    }

    public void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(pointer.position, pointer.forward, out hit, maxGrabbingDistance, Grabable) && hand.currentAttachedObject == null)
        {
            Grab(hit);
        } 
        else if (Physics.Raycast(pointer.position, pointer.forward, out hit, maxGrabbingDistance, BowGrabable) && hand.currentAttachedObject == null)
        {
            GrabBow(GameObject.FindGameObjectWithTag("Bow"));
        }
        else if (blankScript != null)
        {
            blankScript.gameObject.GetComponent<MeshRenderer>().enabled = false;
            laserPointer.thickness = 0.0f;
        }
    }

    private void LateUpdate()
    {
        if (isAttached)
        {
            hand.AttachObject(attachedObj, GrabTypes.Pinch, attachmentOffset: attachedObj.GetComponent<Throwable>().attachmentOffset);
            attachedObj = null;
            isAttached = false;
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject.layer == 11)
        {
            hand.ShowGrabHint("Grab " + e.target.gameObject.name);
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject.layer == 11)
        {
            hand.HideGrabHint();
        }
    }

    public void Grab(RaycastHit hit)
    {
        Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
        SteamVR_Input_Sources inputSource = hand.handType;
        if (hand.grabPinchAction[inputSource].state == true)
        {
            if (interactable != null)
            {
                interactable.transform.LookAt(transform);
                interactable.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 500, ForceMode.Force);
                attachedObj = interactable.gameObject;
                isAttached = true;
            }
        }

        blankScript = hit.collider.gameObject.GetComponentInChildren<BlankScript>();
        blankScript.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        laserPointer.thickness = 0.001f;
    }

    public void GrabBow(GameObject obj)
    {
        Debug.LogError("Object: " + obj.name);

        Interactable interactable = obj.GetComponent<Interactable>();
        SteamVR_Input_Sources inputSource = hand.handType;
        if (hand.grabPinchAction[inputSource].state == true)
        {
            if (interactable != null)
            {
                interactable.transform.LookAt(transform);
                interactable.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 500, ForceMode.Force);
                attachedObj = interactable.gameObject;
                isAttached = true;
            }
        }

        blankScript = obj.GetComponentInChildren<BlankScript>();
        blankScript.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        laserPointer.thickness = 0.001f;
    }
}
