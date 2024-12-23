using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BaseballBatRack : MonoBehaviour, IInteractable, IActivatable {

    [SerializeField] private BaseballBat playerBaseballBatItem;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject baseballBatPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private ItemSO baseballBatSO;
    [SerializeField] private Material[] materials;

    private float spawnTimer;
    private GameObject baseballBat;
    private ItemSO.Type type;
    private Material material;
    private bool isActive;

    private void Update() {
        if (!isActive) {
            return;
        }

        if (baseballBat == null) {
            if (spawnTimer <= 0) {
                GenerateData();
                baseballBat = Instantiate(baseballBatPrefab, spawnPosition);
                baseballBat.GetComponentInChildren<Renderer>().material = material;
                spawnTimer = spawnRate;
            } else {
                spawnTimer -= Time.deltaTime;
            }
        }
    }

    public void Interact() {
        if (!isActive) {
            return;
        }

        if (baseballBat != null && !Player.Instance.HasItemEquipped()) {
            baseballBatSO.type = type;
            baseballBatSO.material = material;
            Player.Instance.Equip(playerBaseballBatItem);

            Destroy(baseballBat);
            baseballBat = null;
        }
    }

    private void GenerateData() {
        float random = Random.Range(0f, 1f);
        if (random < UpgradeSystem.Instance.GetSpecialProbability(UpgradeSystem.UpgradeItem.BASEBALL)) {
            type = ItemSO.Type.SPECIAL;
        } else if (random < UpgradeSystem.Instance.GetNiceProbability(UpgradeSystem.UpgradeItem.BASEBALL)) {
            type = ItemSO.Type.NICE;
        } else {
            type = ItemSO.Type.NAUGHTY;
        }
        material = materials[Random.Range(0, materials.Length)];
    }

    public void Activate(bool isActive) {
        this.isActive = isActive;
    }
}
