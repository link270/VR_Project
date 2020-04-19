using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SimpleAttach : MonoBehaviour
{
    private Interactable interactable;
    private bool isHandAssigned;
    public Hand myHand;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        isHandAssigned = false;
    }

    private void OnHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
    }

    private void OnHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }

    public void DetachSelfFromHand(){
        Debug.Log("****InsideDetatchSelf");
        if(isHandAssigned){
            myHand.DetachObject(gameObject);
        }
    }

    private void HandHoverUpdate(Hand hand)
    {
        
        GrabTypes grabType = hand.GetGrabStarting();
        myHand = hand;
        isHandAssigned = true;
        bool isGrabing = hand.IsGrabEnding(gameObject);

        if (interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType);
            hand.HoverLock(interactable);
        }
        else if (isGrabing)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }
    }
}
