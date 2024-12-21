using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private float jumpCooldown = .2f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float rotationSpeed = 1f;

    private Rigidbody rb;
    private float jumpCooldownTimer;
    

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        HandleMovement();
        HandleJumping();
        HandleRotation();
    }

    private void HandleMovement() {
        Vector2 moveVector = moveSpeed * Time.deltaTime * InputSystem.Instance.GetMoveVector();
        float angle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        float dx = moveVector.x * Mathf.Cos(angle) - moveVector.y * Mathf.Sin(angle);
        float dy = moveVector.x * Mathf.Sin(angle) + moveVector.y * Mathf.Cos(angle);
        rb.velocity = new Vector3(dx, rb.velocity.y, dy);
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log(transform.rotation.eulerAngles.y + " | " + angle);
        }
    }

    private void HandleJumping() {
        if (InputSystem.Instance.GetJump() && jumpCooldownTimer <= 0f) {
            rb.AddForce(jumpPower * -Physics.gravity);
            jumpCooldownTimer = jumpCooldown;
        }
    }

    private void HandleRotation() {
        Vector2 mouseDelta = rotationSpeed * Time.deltaTime * InputSystem.Instance.GetMousePositionDelta();
        //Vector2 mousePosition = (Vector2) Input.mousePosition - new Vector2(Screen.width, Screen.height) / 2;
        //if (!(mousePosition.x < 0 && rotationVector.x < 0 || mousePosition.x > 0 && rotationVector.x > 0)) {
        //    rotationVector.x = 0;
        //}
        //if (!(mousePosition.y < 0 && rotationVector.y < 0 || mousePosition.y > 0 && rotationVector.y > 0)) {
        //    rotationVector.y = 0;
        //}
        Vector3 rotationVector = transform.rotation.eulerAngles + new Vector3(-mouseDelta.y, mouseDelta.x, 0);
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void OnCollisionStay(Collision collision) {
        if (LayerInMask(collision.gameObject.layer, groundLayerMask) && jumpCooldown > 0) {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    private bool LayerInMask(int layer, LayerMask mask) {
        return groundLayerMask == (1 << layer);
    }
}
