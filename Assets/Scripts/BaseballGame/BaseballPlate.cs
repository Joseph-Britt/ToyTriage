using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BaseballPlate : MonoBehaviour, IInteractable {

    [SerializeField] private Transform playerHittingPosition;
    [SerializeField] private BaseballPitchingMachine pitchingMachine;
    [SerializeField] private CinemachineVirtualCamera battingCamera;
    [SerializeField] private BaseballBat playerBaseballBatItem;
    [SerializeField] private float cameraDelay = 1f;

    private float timer;
    private bool finished;

    private void Update() {
        if (timer > 0) {
            timer -= Time.deltaTime;
        } else if (finished) {
            EndInteraction();
            finished = false;
        }
    }

    public void Interact() {
        Player.Instance.StartInteraction(EndInteraction);
        Player.Instance.GetVisual().transform.SetPositionAndRotation(playerHittingPosition.position, playerHittingPosition.rotation);
        
        battingCamera.Priority = 11;
        pitchingMachine.SetEnabled(true);
        playerBaseballBatItem.OnHit += PlayerBaseballBatItem_OnHit;
    }

    private void PlayerBaseballBatItem_OnHit(object sender, System.EventArgs e) {
        timer = cameraDelay;
        finished = true;
    }

    private void EndInteraction() {
        Player.Instance.transform.SetPositionAndRotation(playerHittingPosition.position, playerHittingPosition.rotation);
        Player.Instance.GetVisual().transform.SetPositionAndRotation(playerHittingPosition.position, playerHittingPosition.rotation);
        
        battingCamera.Priority = 0;
        pitchingMachine.SetEnabled(false);
        playerBaseballBatItem.OnHit -= PlayerBaseballBatItem_OnHit;

        Player.Instance.EndInteraction();
    }
}
