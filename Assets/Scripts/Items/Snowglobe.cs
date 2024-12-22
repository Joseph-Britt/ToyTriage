using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowglobe : Item {

    [SerializeField] private ParticleSystem snowParticles;
    [SerializeField] private float shakeTime = 1f;
    [SerializeField] private float snowTime = 5f;
    [SerializeField] private Renderer[] visualsToColor;

    private enum State {
        HOLDING,
        SHAKING,
        SNOWING
    }

    private Animator animator;
    private const string SHAKE = "Shake";
    private float snowTimer;
    private State state;

    private void Awake() {
        animator = GetComponent<Animator>();
        state = State.HOLDING;
    }

    public override void HandleUse() {
        switch (state) {
            case State.HOLDING:
                if (InputSystem.Instance.GetMouseButtonDown(0)) {
                    animator.SetTrigger(SHAKE);
                    state = State.SHAKING;
                    snowTimer = shakeTime;
                }
                break;
            case State.SHAKING:
                if (snowTimer > 0) {
                    snowTimer -= Time.deltaTime;
                } else {
                    state = State.SNOWING;
                    snowTimer = snowTime;
                    snowParticles.Play();
                }
                break;
            case State.SNOWING:
                if (snowTimer > 0) {
                    snowTimer -= Time.deltaTime;
                } else {
                    state = State.HOLDING;
                    snowParticles.Stop();
                }
                break;
        }
    }

    public override void Equip() {
        gameObject.SetActive(true);
        foreach (Renderer visual in visualsToColor) {
            visual.material = GetData().material;
        }
    }

    public override void Unequip() {
        gameObject.SetActive(false);
    }
}
