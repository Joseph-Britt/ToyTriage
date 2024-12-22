using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BaseballBatRack : MonoBehaviour, IInteractable {

    [SerializeField] private BaseballBat playerBaseballBatItem;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject baseballBatPrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private ItemSO baseballBatSO;
    [SerializeField] private Material[] materials;
    [SerializeField] private float niceProbability = .8f;

    private float spawnTimer;
    private GameObject baseballBat;
    private bool isNice;
    private Material material;

    private void Update() {
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
        if (baseballBat != null && !Player.Instance.HasItemEquipped()) {
            baseballBatSO.isNice = isNice;
            baseballBatSO.material = material;
            Player.Instance.Equip(playerBaseballBatItem);

            Destroy(baseballBat);
            baseballBat = null;
        }
    }

    private void GenerateData() {
        isNice = Random.Range(0f, 1f) < niceProbability;
        material = materials[Random.Range(0, materials.Length)];
    }
}
