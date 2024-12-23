using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    [SerializeField] protected ItemSO data;
    [SerializeField] protected Renderer[] visualsToColor;

    protected Animator animator;
    protected float timer;

    protected virtual void Awake() {
        animator = GetComponent<Animator>();
    }

    public abstract void HandleUse();

    public virtual void Equip() {
        gameObject.SetActive(true);
        foreach (Renderer visual in visualsToColor) {
            visual.material = GetData().material;
        }
    }

    public virtual void Unequip() {
        gameObject.SetActive(false);
    }

    public ItemSO GetData() {
        return data;
    }

    public void SetData(ItemSO data) {
        this.data = data;
    }
}
