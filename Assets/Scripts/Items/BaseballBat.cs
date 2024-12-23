using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : Item {

    public event EventHandler<EventArgs> OnHit;

    [SerializeField] private float hitCastMaxDistance = 0.5f;
    [SerializeField] private LayerMask hitCheckLayerMask;
    [SerializeField] private float swingTime = 2f;
    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject broken;

    private enum State {
        IDLE,
        SWINGING
    }

    private const string SWING = "Swing";
    private State state;

    protected override void Awake() {
        base.Awake();
        OnHit = new EventHandler<EventArgs>(OnHit);
    }

    public override void HandleUse() {
        switch (state) {
            case State.IDLE:
                if (InputSystem.GetMouseButton(0)) {
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
        base.Equip();
        state = State.IDLE;

        // Update visuals
        visual.SetActive(true);
        broken.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out Baseball baseball)) {
            if (Physics.Raycast(other.transform.position, -baseball.GetComponent<Rigidbody>().velocity, out RaycastHit _, hitCastMaxDistance, hitCheckLayerMask)) {
                return;
            }
            baseball.Hit();
            if (data.type == ItemSO.Type.NAUGHTY) {
                visual.SetActive(false);
                broken.SetActive(true);
            }
            OnHit?.Invoke(this, EventArgs.Empty);
        }
    }
}
