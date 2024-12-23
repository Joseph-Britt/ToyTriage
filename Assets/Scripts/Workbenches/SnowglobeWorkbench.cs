using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowglobeWorkbench : MonoBehaviour, IInteractable, IActivatable {

    [SerializeField] private Snowglobe playerSnowglobeItem;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject snowglobePrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private ItemSO snowglobeSO;
    [SerializeField] private Material[] materials;
    [SerializeField] private Gradient[] gradients;

    private float spawnTimer;
    private GameObject snowglobe;
    private ItemSO.Type type;
    private Material material;
    private Gradient gradient;
    private bool isActive;

    private void Update() {
        if (!isActive) {
            return;
        }

        if (snowglobe == null) {
            if (spawnTimer <= 0) {
                snowglobe = Instantiate(snowglobePrefab, spawnPosition);
                GenerateData();
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

        if (snowglobe != null && !Player.Instance.HasItemEquipped()) {
            Debug.Log(type);
            snowglobeSO.type = type;
            snowglobeSO.material = material;
            snowglobeSO.gradient = gradient;
            Player.Instance.Equip(playerSnowglobeItem);
            
            Destroy(snowglobe);
            snowglobe = null;
        }
    }

    private void GenerateData() {
        float random = Random.Range(0f, 1f);
        if (random < UpgradeSystem.Instance.GetSpecialProbability(UpgradeSystem.UpgradeItem.SNOWGLOBE)) {
            type = ItemSO.Type.SPECIAL;
        } else if (random < UpgradeSystem.Instance.GetNiceProbability(UpgradeSystem.UpgradeItem.SNOWGLOBE)) {
            type = ItemSO.Type.NICE;
        } else {
            type = ItemSO.Type.NAUGHTY;
        }
        material = materials[Random.Range(0, materials.Length)];
        gradient = gradients[Random.Range(0, gradients.Length)];
    }

    public void Activate(bool isActive) {
        this.isActive = isActive;
    }
}
