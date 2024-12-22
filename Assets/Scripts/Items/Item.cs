using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    [SerializeField] private GameObject visual;

    public abstract void HandleUse();

    public void Equip() {
        visual.SetActive(true);
    }

    public void Unequip() {
        visual.SetActive(false);
    }
}
