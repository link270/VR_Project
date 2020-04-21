using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;
using Valve.VR;

public class RemoteGrab : MonoBehaviour
{
    private SteamVR_LaserPointer laserPointer;
    private Transform pointer;
    private Hand hand;
    private GameObject attachedObj;
    private bool isAttached;

    public bool showHint = true;
    public GameObject PointerObject;
    public LayerMask Grabable;
    public LayerMask BowGrabable;
    public float maxGrabbingDistance = 15f;
    private BlankScript blankScript;

    void Start()
    {
        hand = GetComponent<Hand>();
        isAttached = false;
        attachedObj = null;
        pointer = PointerObject.GetComponent<Transform>();
        laserPointer = PointerObject.GetComponent<SteamVR_LaserPointer>();
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.thickness = 0.000f;
    }

    public void Update()
    {
        if(showHint) hand.ShowGripHint("Activate Remote Grab");
        RaycastHit hit;
        if (hand.grabGripAction[hand.handType].state == true)
        {
            if (showHint)
            {
                showHint = false;
                hand.HideGripHint();
            }
            laserPointer.thickness = 0.002f;
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
                laserPointer.color = Color.black;
                blankScript.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            laserPointer.thickness = 0.0f;
        }
    }

    private void LateUpdate()
    {
        if (isAttached)
        {
            if (gameObject.name == "RightHand")
            {
                hand.AttachObject(attachedObj, GrabTypes.Pinch, attachmentOffset: attachedObj.GetComponent<Throwable>().rightAttachmentOffset);
            }
            else
            {
                hand.AttachObject(attachedObj, GrabTypes.Pinch, attachmentOffset: attachedObj.GetComponent<Throwable>().attachmentOffset);
            }
            
            attachedObj = null;
            isAttached = false;
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject.layer == 11)
        {
            hand.ShowPinchHint("Grab " + e.target.gameObject.name);
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject.layer == 11)
        {
            hand.HidePinchHint();
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
        laserPointer.color = Color.yellow;
    }

    public void GrabBow(GameObject obj)
    {
        //Debug.LogError("Object: " + obj.name);

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
        laserPointer.color = Color.yellow;
    }
}
