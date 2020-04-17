using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;
using Valve.VR;

public class RemoteGrab : MonoBehaviour
{
    private SteamVR_LaserPointer pointer;
    private Hand hand;

    public float maxGrabbingDistance = 15f;

    void Awake()
    {
        hand = GetComponent<Hand>();
        pointer = GetComponent<SteamVR_LaserPointer>();
        pointer.PointerIn += PointerInside;
        pointer.PointerOut += PointerOutside;
        pointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        float distance = (e.target.gameObject.GetComponent<Transform>().position - gameObject.transform.position).magnitude;
        if (e.target.gameObject.GetComponent<SimpleAttach>() != null && distance <= maxGrabbingDistance)
        {
            Grab(e.target.gameObject);
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        
        if (e.target.gameObject.GetComponent<SimpleAttach>() != null)
        {
            hand.ShowGrabHint("Grab " + e.target.gameObject.name);
            //var t = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
            //t.GetStateDown( SteamVR_Input_Source.GetSource( hand);
            //Debug.Log("State? " + t);
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.gameObject.GetComponent<SimpleAttach>() != null)
        {
            hand.HideGrabHint();
        }
    }

    public void Grab(GameObject obj)
    {
        Interactable interactable = obj.GetComponent<Interactable>();
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabing = hand.IsGrabEnding(obj);
        //Debug.Log("Grabbing: " + isGrabing + " Type: " + grabType + " obj: " + obj.name+" Att: "+interactable.attachedToHand);
        //hand.AttachObject(obj, grabType);

        if (interactable.attachedToHand == null)
        {
            hand.AttachObject(obj, grabType);
            hand.HoverLock(interactable);
        }
        else if (isGrabing)
        {
            hand.DetachObject(obj);
            hand.HoverUnlock(interactable);
        }
    }
}
