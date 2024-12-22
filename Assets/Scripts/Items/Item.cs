using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    [SerializeField] private ItemSO data;

    public abstract void HandleUse();
    public abstract void Equip();
    public abstract void Unequip();

    public ItemSO GetData() {
        return data;
    }

    public void SetData(ItemSO data) {
        this.data = data;
    }
}
