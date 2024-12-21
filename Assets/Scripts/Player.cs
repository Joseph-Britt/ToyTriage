using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private float jumpCooldown = .2f;
    [SerializeField] private LayerMask groundLayerMask;
    [Header("Interaction")]
    [SerializeField] private Transform holdPoint;

    private Rigidbody rb;
    private float jumpCooldownTimer;
    private List<IInteractable> interactables;
    private GameObject item;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There is more than one Player " + Instance);
        }
        rb = GetComponent<Rigidbody>();
        interactables = new List<IInteractable>();
    }

    private void Update() {
        // Movement
        HandleMovement();
        HandleRotation();
        //HandleJumping();

        // Interaction
        HandleInteraction();
    }

    private void HandleMovement() {
        Vector2 moveVector = Time.deltaTime * InputSystem.Instance.GetMoveVector();
        moveVector *= InputSystem.Instance.IsSprinting() ? sprintSpeed : moveSpeed;
        float angle = -transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        float dx = moveVector.x * Mathf.Cos(angle) - moveVector.y * Mathf.Sin(angle);
        float dy = moveVector.x * Mathf.Sin(angle) + moveVector.y * Mathf.Cos(angle);
        rb.velocity = new Vector3(dx, rb.velocity.y, dy);
    }

    private void HandleRotation() {
        Vector2 mouseDelta = rotationSpeed * Time.deltaTime * InputSystem.Instance.GetMousePositionDelta();
        Vector3 rotationVector = transform.rotation.eulerAngles + new Vector3(-mouseDelta.y, mouseDelta.x, 0);
        transform.rotation = Quaternion.Euler(rotationVector);
    }

    private void HandleJumping() {
        if (InputSystem.Instance.IsJumping() && jumpCooldownTimer <= 0f) {
            rb.AddForce(jumpPower * -Physics.gravity);
            jumpCooldownTimer = jumpCooldown;
        }
    }

    private void HandleInteraction() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (interactables.Count > 0) {
                interactables[interactables.Count() - 1].Interact();
            }
        }
    }

    public void Equip(GameObject item) {
        this.item = item;
        item.transform.parent = holdPoint;
        Debug.Log("Equipped " + item.name);
    }

    public bool HasItemEquipped() {
        return item != null;
    }

    private void OnCollisionStay(Collision collision) {
        if (LayerInMask(collision.gameObject.layer, groundLayerMask) && jumpCooldown > 0) {
            jumpCooldownTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out IInteractable interactable)) {
            interactables.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.TryGetComponent(out IInteractable interactable)) {
            interactables.Remove(interactable);
        }
    }

    private bool LayerInMask(int layer, LayerMask mask) {
        return groundLayerMask == (1 << layer);
    }
}
