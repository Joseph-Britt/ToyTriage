using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBatRack : MonoBehaviour, IInteractable {

    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject baseballBatPrefab;
    [SerializeField] private Transform spawnPosition;

    private float spawnTimer;
    private GameObject baseballBat;

    private void Update() {
        if (baseballBat == null) {
            if (spawnTimer <= 0) {
                baseballBat = Instantiate(baseballBatPrefab, spawnPosition);
                spawnTimer = spawnRate;
            } else {
                spawnTimer -= Time.deltaTime;
            }
        }
    }

    public void Interact() {
        if (baseballBat != null && !Player.Instance.HasItemEquipped()) {
            Player.Instance.Equip(baseballBat);
            baseballBat = null;
        }
    }
}
