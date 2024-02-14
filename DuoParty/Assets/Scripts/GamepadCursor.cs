using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class GamepadCursor : MonoBehaviour
{
    [SerializeField]
    private float offset;
    private VirtualMouseInput virtualMouseInput;

    private void Awake()
    {
        virtualMouseInput = GetComponent<VirtualMouseInput>();
    }

    private void LateUpdate()
    {
        Vector2 virtualMousePosition = virtualMouseInput.virtualMouse.position.value;
        virtualMousePosition.x = Mathf.Clamp(virtualMousePosition.x, offset, Screen.width- offset);
        virtualMousePosition.y = Mathf.Clamp(virtualMousePosition.y, offset, Screen.height- offset);
        InputState.Change(virtualMouseInput.virtualMouse.position, virtualMousePosition);
    }
}
