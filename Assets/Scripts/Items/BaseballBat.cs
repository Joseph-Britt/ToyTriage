using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : Item {

    public event EventHandler<EventArgs> OnHit;

    [SerializeField] private float hitCastMaxDistance = 0.5f;
    [SerializeField] private LayerMask hitCheckLayerMask;
    [SerializeField] private float swingTime = 2f;
    [SerializeField] private Renderer[] visualsToColor;

    private enum State {
        IDLE,
        SWINGING
    }

    private Animator animator;
    private const string SWING = "Swing";
    private float timer;
    private State state;

    private void Awake() {
        animator = GetComponent<Animator>();
        OnHit = new EventHandler<EventArgs>(OnHit);
    }

    public override void HandleUse() {
        switch (state) {
            case State.IDLE:
                if (InputSystem.Instance.GetMouseButton(0)) {
                    animator.SetTrigger(SWING);
                    state = State.SWINGING;
                    timer = swingTime;
                }
                break;
            case State.SWINGING:
                if (timer > 0) {
                    timer -= Time.deltaTime;
                } else {
                    state = State.IDLE;
                }
                break;
        }
    }

    public override void Equip() {
        gameObject.SetActive(true);
        state = State.IDLE;
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
            if (!GetData().isNice) {
                Debug.Log("Bat broke!");
            }
            OnHit?.Invoke(this, EventArgs.Empty);
        }
    }
}
