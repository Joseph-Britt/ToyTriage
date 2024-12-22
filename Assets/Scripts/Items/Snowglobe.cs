using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowglobe : Item {

    private Animator animator;
    private const string SHAKE = "Shake";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public override void HandleUse() {
        if (InputSystem.Instance.GetMouseButtonDown(0)) {
            animator.SetTrigger(SHAKE);
        }
    }
}
