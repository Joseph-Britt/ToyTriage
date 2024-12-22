using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {
    
    public static InputSystem Instance { get; private set; }

    private Queue<Vector2> previousMousePositions;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There is more than one InputSystem " + Instance);
        }
        previousMousePositions = new Queue<Vector2>();
    }

    private void Update() {
        previousMousePositions.Enqueue(Input.mousePosition);
        if (previousMousePositions.Count > 2) {
            previousMousePositions.Dequeue();
        }
    }

    public Vector2 GetMoveVector() {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    public bool IsJumping() {
        return Input.GetButton("Jump");
    }

    public Vector2 GetMousePositionDelta() {
        if (previousMousePositions.Count < 2) {
            return Vector2.zero;
        }
        return (Vector2) Input.mousePosition - previousMousePositions.Peek();
    }

    public bool IsSprinting() {
        return Input.GetKey(KeyCode.LeftShift);
    }

    public bool GetMouseButtonDown(int button) {
        return Input.GetMouseButtonDown(button);
    }

    public bool GetMouseButtonUp(int button) {
        return Input.GetMouseButtonUp(button);
    }
}
