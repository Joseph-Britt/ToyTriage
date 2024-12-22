using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowglobe : Item {

    [SerializeField] private ParticleSystem snowParticles;
    [SerializeField] private float shakeTime = 1f;
    [SerializeField] private float snowTime = 5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private Renderer[] visualsToColor;

    private enum State {
        IDLE,
        SHAKING,
        SNOWING,
        COOLDOWN
    }

    private Animator animator;
    private const string SHAKE = "Shake";
    private float timer;
    private State state;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public override void HandleUse() {
        switch (state) {
            case State.IDLE:
                if (InputSystem.Instance.GetMouseButton(0)) {
                    animator.SetTrigger(SHAKE);
                    state = State.SHAKING;
                    timer = shakeTime;
                }
                break;
            case State.SHAKING:
                if (timer > 0) {
                    timer -= Time.deltaTime;
                } else {
                    if (!GetData().isNice) {
                        // Defective
                        state = State.COOLDOWN;
                    } else {
                        state = State.SNOWING;
                        timer = snowTime;
                        snowParticles.Play();
                    }
                }
                break;
            case State.SNOWING:
                if (timer > 0) {
                    timer -= Time.deltaTime;
                } else {
                    state = State.COOLDOWN;
                    timer = cooldown;
                    snowParticles.Stop();
                }
                break;
            case State.COOLDOWN:
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
        ParticleSystem.MainModule main = snowParticles.main;
        ParticleSystem.MinMaxGradient gradient = new ParticleSystem.MinMaxGradient(GetData().gradient);
        gradient.mode = ParticleSystemGradientMode.RandomColor;
        main.startColor = gradient;
    }

    public override void Unequip() {
        gameObject.SetActive(false);
    }
}
