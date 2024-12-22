using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowglobeWorkbench : MonoBehaviour, IInteractable {

    [SerializeField] private Snowglobe playerSnowglobeItem;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject snowglobePrefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private ItemSO snowglobeSO;
    [SerializeField] private Material[] materials;
    [SerializeField] private float niceProbability = 0.6f;

    private float spawnTimer;
    private GameObject snowglobe;
    private bool isNice;
    private Material material;

    private void Update() {
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
        if (snowglobe != null && !Player.Instance.HasItemEquipped()) {
            snowglobeSO.isNice = isNice;
            snowglobeSO.material = material;
            Player.Instance.Equip(playerSnowglobeItem);
            
            Destroy(snowglobe);
            snowglobe = null;
        }
    }

    private void GenerateData() {
        isNice = Random.Range(0f, 1f) < niceProbability;
        material = materials[Random.Range(0, materials.Length)];
        Debug.Log("next: " + isNice + ", curr: " + snowglobeSO.isNice);
    }
}
