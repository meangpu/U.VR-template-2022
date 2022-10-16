using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabFar : XRGrabInteractable
{
    void Start()
    {
        if (!attachTransform)
        {
            GameObject attachPoint = new GameObject("offset Grab pivot");
            attachPoint.transform.SetParent(transform, false);
            attachTransform = attachPoint.transform;
        }

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        attachTransform.position = args.interactableObject.transform.position;
        attachTransform.rotation = args.interactableObject.transform.rotation;
        base.OnSelectEntered(args);
    }
}
