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


    private void Update()
    {
        bool isLeftPress = leftActivate.action.ReadValue<float>() > 0.1f;
        bool isRightPress = rightActivate.action.ReadValue<float>() > 0.1f;

        leftTeleportRay.SetActive(isLeftPress);
        rightTeleportRay.SetActive(isRightPress);

    }
}
