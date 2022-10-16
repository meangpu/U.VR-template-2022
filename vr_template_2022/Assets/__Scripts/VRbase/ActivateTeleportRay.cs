using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportRay : MonoBehaviour
{

    [SerializeField] GameObject leftTeleportRay;
    [SerializeField] GameObject rightTeleportRay;

    [SerializeField] InputActionProperty leftActivate;
    [SerializeField] InputActionProperty rightActivate;

    [SerializeField] InputActionProperty leftCancel;
    [SerializeField] InputActionProperty rightCancel;


    private void Update()
    {
        bool isLeftPress = leftActivate.action.ReadValue<float>() > 0.1f;
        bool isRightPress = rightActivate.action.ReadValue<float>() > 0.1f;

        bool isLeftNotGrabing = leftCancel.action.ReadValue<float>() == 0;
        bool isRightNotGrabing = rightCancel.action.ReadValue<float>() == 0;

        leftTeleportRay.SetActive(isLeftNotGrabing && isLeftPress);
        rightTeleportRay.SetActive(isRightNotGrabing && isRightPress);

    }
}
