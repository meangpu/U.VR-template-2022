using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class ActivateTeleportRay : MonoBehaviour
{

    [SerializeField] GameObject leftTeleportRay;
    [SerializeField] GameObject rightTeleportRay;

    [SerializeField] InputActionProperty leftActivate;
    [SerializeField] InputActionProperty rightActivate;

    [SerializeField] InputActionProperty leftCancel;
    [SerializeField] InputActionProperty rightCancel;

    [SerializeField] XRRayInteractor leftRay;
    [SerializeField] XRRayInteractor rightRay;

    private void Update()
    {
        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftNumber, out bool leftValid);
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightNumber, out bool rightValid);

        bool isLeftPress = leftActivate.action.ReadValue<float>() > 0.1f;
        bool isRightPress = rightActivate.action.ReadValue<float>() > 0.1f;

        bool isLeftNotGrabing = leftCancel.action.ReadValue<float>() < 0.1f;
        bool isRightNotGrabing = rightCancel.action.ReadValue<float>() < 0.1f;

        leftTeleportRay.SetActive(!isLeftRayHovering && isLeftNotGrabing && isLeftPress);
        rightTeleportRay.SetActive(!isRightRayHovering && isRightNotGrabing && isRightPress);

    }
}
