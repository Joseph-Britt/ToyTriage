using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private float jumpCooldown = .2f;
    [SerializeField] private LayerMask groundLayerMask;
    [Header("Interaction")]
    [SerializeField] private GameObject playerVisual;

    private Rigidbody rb;
    private float jumpCooldownTimer;
    private List<IInteractable> interactables;
    private Item item;
    private bool inInteraction;
    private Action OnExitInteraction;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There is more than one Player " + Instance);
        }
        rb = GetComponent<Rigidbody>();
        interactables = new List<IInteractable>();
        inInteraction = false;
    }

    private void Update() {
        // Movement
        if (!inInteraction) {
            HandleMovement();
            HandleRotation();
            //HandleJumping();
        }

        // Interaction
        HandleInteraction();

        // Items
        HandleItemUse();
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

        // Rotate child camera only for xRotation
        Vector3 xRotation = playerCamera.transform.rotation.eulerAngles + new Vector3(-mouseDelta.y, 0, 0);
        playerCamera.transform.rotation = Quaternion.Euler(xRotation);

        // Rotate parent for yRotation
        Vector3 yRotation = transform.rotation.eulerAngles + new Vector3(0, mouseDelta.x, 0);
        transform.rotation = Quaternion.Euler(yRotation);
    }

    private void HandleJumping() {
        if (InputSystem.Instance.IsJumping() && jumpCooldownTimer <= 0f) {
            rb.AddForce(jumpPower * -Physics.gravity);
            jumpCooldownTimer = jumpCooldown;
        }
    }

    private void HandleInteraction() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (inInteraction) {
                ExitInteractionEarly();
            } else if (interactables.Count > 0) {
                interactables[interactables.Count() - 1].Interact();
            }
        }
    }

    private void HandleItemUse() {
        if (item != null) {
            item.HandleUse();
        }
    }

    public void Equip(Item item) {
        if (item != null) {
            this.item = item;
            item.Equip();
        }
    }

    public Item Unequip() {
        Item removed = item;
        if (item != null) {
            item.Unequip();
            item = null;
        }
        return removed;
    }

    public bool HasItemEquipped() {
        return item != null;
    }

    // Interaction calls when beginning
    public void StartInteraction(Action OnExitInteraction) {
        inInteraction = true;
        rb.velocity = Vector3.zero;
        this.OnExitInteraction = OnExitInteraction;
    }

    // Interaction calls when complete
    public void EndInteraction() {
        inInteraction = false;
    }

    // Used for when the player cancels an interaction early
    private void ExitInteractionEarly() {
        OnExitInteraction?.Invoke();
        OnExitInteraction = null;
        inInteraction = false;
    }
    
    public GameObject GetVisual() {
        return playerVisual;
    }

    private void OnCollisionStay(Collision collision) {
        if (Utils.LayerInMask(collision.gameObject.layer, groundLayerMask) && jumpCooldown > 0) {
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
}
