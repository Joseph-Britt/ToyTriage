using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryStation : MonoBehaviour, IInteractable {

    public void Interact() {
        if (Player.Instance.HasItemEquipped()) {
            Item item = Player.Instance.Unequip();
            ItemSO data = item.GetData();
            if (data.type == ItemSO.Type.SPECIAL) {
                ScoreSystem.Instance.Reward(data.guessedNiceReward * 4);
            } else if (data.type == ItemSO.Type.NICE) {
                ScoreSystem.Instance.Reward(data.guessedNiceReward);
            } else {
                ScoreSystem.Instance.Reward(data.guessedNicePenalty);
            }
        }
    }
}
