using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuToggle : MonoBehaviour
{
    [SerializeField] GameObject menu;
    public InputActionProperty showButton;

    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }

    }
}
