using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JackInTheBox : Item {

    [SerializeField] private float playTime = 2f;
    [SerializeField] private float jumpScareTime = 0.5f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float jumpscareDelayMin = 0.5f;
    [SerializeField] private float jumpscareDelayMax = 2f;

    private enum State {
        IDLE,
        PLAYING,
        JUMPSCARE_DELAY,
        JUMPSCARE,
        COOLDOWN
    }

    private const string PLAY = "Play";
    private const string PLAY_WITHOUT_OPEN = "PlayWithoutOpen";
    private const string JUMPSCARE = "Jumpscare";
    private State state;

    public override void HandleUse() {
        switch (state) {
            case State.IDLE:
                if (InputSystem.GetMouseButton(0)) {
                    if (data.type == ItemSO.Type.NICE) {
                        animator.SetTrigger(PLAY);
                    } else {
                        animator.SetTrigger(PLAY_WITHOUT_OPEN);
                    }
                    state = State.PLAYING;
                    timer = playTime;
                }
                break;
            case State.PLAYING:
                if (timer > 0) {
                    timer -= Time.deltaTime;
                    if (InputSystem.GetMouseButton(0)) {
                        animator.speed = 1f;
                    } else {
                        animator.speed = 0f;
                    }
                } else {
                    if (data.type == ItemSO.Type.SPECIAL) {
                        state = State.JUMPSCARE_DELAY;
                        timer = Random.Range(jumpscareDelayMin, jumpscareDelayMax);
                    } else {
                        state = State.COOLDOWN;
                        timer = cooldown;
                    }
                    animator.speed = 1f;
                }
                break;
            case State.JUMPSCARE_DELAY:
                if (timer > 0) {
                    timer -= Time.deltaTime;
                } else {
                    state = State.JUMPSCARE;
                    animator.SetTrigger(JUMPSCARE);
                    timer = jumpScareTime;
                }
                break;
            case State.JUMPSCARE:
                if (timer > 0) {
                    timer -= Time.deltaTime;
                } else {
                    state = State.COOLDOWN;
                    timer = cooldown;
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
        base.Equip();
        state = State.IDLE;
    }
}
