using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingBin : MonoBehaviour, IInteractable {

    public void Interact() {
        if (Player.Instance.HasItemEquipped()) {
            Item item = Player.Instance.Unequip();
            ItemSO data = item.GetData();
            if (!data.isNice) {
                Debug.Log(data.guessedNaughtyReward);
            } else {
                Debug.Log(data.guessedNaughtyPenalty);
            }
        }
    }
}
