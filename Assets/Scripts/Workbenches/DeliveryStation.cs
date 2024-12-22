using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryStation : MonoBehaviour, IInteractable {

    public void Interact() {
        Debug.Log("Item equipped? " + Player.Instance.HasItemEquipped());
        if (Player.Instance.HasItemEquipped()) {
            Item item = Player.Instance.Unequip();
            ItemSO data = item.GetData();
            if (data.isNice) {
                Debug.Log(data.guessedNiceReward);
            } else {
                Debug.Log(data.guessedNicePenalty);
            }
        }
    }
}
