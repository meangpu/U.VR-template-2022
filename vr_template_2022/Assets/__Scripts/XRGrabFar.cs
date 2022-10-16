using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabFar : XRGrabInteractable
{
    private Vector3 initialLocalPos;
    private Quaternion initialLocalRot;

    void Start()
    {
        if (!attachTransform)
        {
            GameObject attachPoint = new GameObject("offset Grab pivot");
            attachPoint.transform.SetParent(transform, false);
            attachTransform = attachPoint.transform;
        }
        else
        {
            initialLocalPos = attachTransform.localPosition;
            initialLocalRot = attachTransform.localRotation;
        }

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            // this mean grab with hand
            attachTransform.position = args.interactableObject.transform.position;
            attachTransform.rotation = args.interactableObject.transform.rotation;
        }
        else
        {
            // this mean grab with RAY

            attachTransform.position = initialLocalPos;
            attachTransform.rotation = initialLocalRot;
        }
        base.OnSelectEntered(args);
    }
}
