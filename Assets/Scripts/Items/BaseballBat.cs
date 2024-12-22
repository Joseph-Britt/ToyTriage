using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : Item {

    public event EventHandler<EventArgs> OnHit;

    [SerializeField] private float hitCastMaxDistance = 0.5f;
    [SerializeField] private LayerMask hitCheckLayerMask;
    [SerializeField] private float swingCooldown = 2f;
    [SerializeField] private Renderer[] visualsToColor;

    private Animator animator;
    private const string SWING = "Swing";
    private float swingTimer;

    private void Awake() {
        animator = GetComponent<Animator>();
        OnHit = new EventHandler<EventArgs>(OnHit);
    }

    public void Update() {
        if (swingTimer > 0) {
            swingTimer -= Time.deltaTime;
        }
        Debug.Log(swingTimer);
    }

    public override void HandleUse() {
        if (InputSystem.Instance.GetMouseButtonDown(0) && swingTimer <= 0) {
            animator.SetTrigger(SWING);
            swingTimer = swingCooldown;
        }
    }

    public override void Equip() {
        if (!gameObject.activeSelf) {
            gameObject.SetActive(true);
        }
        foreach (Renderer visual in visualsToColor) {
            visual.material = GetData().material;
        }
    }

    public override void Unequip() {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out Baseball baseball)) {
            if (Physics.Raycast(other.transform.position, -baseball.GetComponent<Rigidbody>().velocity, out RaycastHit _, hitCastMaxDistance, hitCheckLayerMask)) {
                return;
            }
            baseball.Hit();
            OnHit?.Invoke(this, EventArgs.Empty);
        }
    }
}
