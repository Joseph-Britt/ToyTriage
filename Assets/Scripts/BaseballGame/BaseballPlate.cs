using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BaseballPlate : MonoBehaviour, IInteractable {

    [SerializeField] private Transform playerHittingPosition;
    [SerializeField] private BaseballPitchingMachine pitchingMachine;
    [SerializeField] private CinemachineVirtualCamera battingCamera;
    [SerializeField] private BaseballBat playerBaseballBatItem;

    public void Interact() {
        Player.Instance.StartInteraction(EndInteraction);
        Player.Instance.GetVisual().transform.SetPositionAndRotation(playerHittingPosition.position, playerHittingPosition.rotation);
        
        battingCamera.Priority = 11;
        pitchingMachine.SetEnabled(true);
        playerBaseballBatItem.OnHit += PlayerBaseballBatItem_OnHit;
    }

    private void PlayerBaseballBatItem_OnHit(object sender, System.EventArgs e) {
        EndInteraction();
    }

    private void EndInteraction() {
        Player.Instance.transform.SetPositionAndRotation(playerHittingPosition.position, playerHittingPosition.rotation);
        Player.Instance.GetVisual().transform.SetPositionAndRotation(playerHittingPosition.position, playerHittingPosition.rotation);
        
        battingCamera.Priority = 0;
        pitchingMachine.SetEnabled(false);
        playerBaseballBatItem.OnHit -= PlayerBaseballBatItem_OnHit;

        Player.Instance.EndInteraction();
        Player.Instance.Unequip();

        // TODO: update score
    }
}
