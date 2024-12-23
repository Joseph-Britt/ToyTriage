using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {

    public static Vector2 GetMoveVector() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    public static bool IsJumping() {
        return Input.GetButton("Jump");
    }

    public static Vector2 GetMousePositionDelta() {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    public static bool IsSprinting() {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public static bool GetUpgradeMenu() {
        return Input.GetKeyDown(KeyCode.Tab);
    }

    public static bool GetInteract() {
        return Input.GetKeyDown(KeyCode.E);
    }

    public static bool GetPause() {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public static bool GetMouseButtonDown(int button) {
        return Input.GetMouseButtonDown(button);
    }

    public static bool GetMouseButtonUp(int button) {
        return Input.GetMouseButtonUp(button);
    }

    public static bool GetMouseButton(int button) {
        return Input.GetMouseButton(button);
    }
}
