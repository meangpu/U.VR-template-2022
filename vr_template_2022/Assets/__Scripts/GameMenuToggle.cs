using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuToggle : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float spawnDistance = 2;

    [SerializeField] GameObject menu;
    public InputActionProperty showButton;


    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
            Vector3 headLocation = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
            menu.transform.position = headLocation;

        }

        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
