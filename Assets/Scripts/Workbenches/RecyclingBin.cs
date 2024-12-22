using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingBin : MonoBehaviour, IInteractable {

    public void Interact() {
        if (Player.Instance.HasItemEquipped()) {
            Item item = Player.Instance.Unequip();
            ItemSO data = item.GetData();
            if (!data.isNice) {
                ScoreSystem.Instance.Reward(data.guessedNaughtyReward);
            } else {
                ScoreSystem.Instance.Reward(data.guessedNaughtyPenalty);
            }
        }
    }
}
