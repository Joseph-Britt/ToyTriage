using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystemUI : MonoBehaviour {

    public static UpgradeSystemUI Instance { get; private set; }

    [SerializeField] Transform rowContainer;
    [SerializeField] Transform rowTemplate;
    [SerializeField] Transform itemTemplate;
    [SerializeField] UpgradeGroupSO[] upgrades;
    [SerializeField] GameObject[] workbenches;

    private UpgradeItemUI[][] upgradeItemUIs;
    private int[] upgradeLevels;
    private bool isActive;
    private CanvasGroup group;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There is more than one UpgradeSystemUI " + Instance);
        }
        group = GetComponent<CanvasGroup>();
    }

    private void Start() {
        upgradeItemUIs = new UpgradeItemUI[upgrades.Length][];
        upgradeLevels = new int[upgrades.Length];

        for (int i = 0; i < upgrades.Length; i++) {
            UpgradeGroupSO upgradeGroup = upgrades[i];
            Transform row = Instantiate(rowTemplate);
            row.SetParent(rowContainer);
            row.gameObject.SetActive(true);

            upgradeItemUIs[i] = new UpgradeItemUI[upgradeGroup.upgrades.Length];
            upgradeLevels[i] = 0;

            for (int j = 0; j < upgradeItemUIs[i].Length; j++) {
                UpgradeSO upgradeItem = upgradeGroup.upgrades[j];
                Transform item = Instantiate(itemTemplate);
                item.SetParent(row);
                item.gameObject.SetActive(true);
                if (item.TryGetComponent(out UpgradeItemUI upgradeUI)) {
                    upgradeUI.Setup(upgradeItem, i);
                    if (j == 0) {
                        upgradeUI.Activate();
                    }
                    upgradeItemUIs[i][j] = upgradeUI;
                }
            }
        }
    }

    public void Activate(bool isActive) {
        this.isActive = isActive;
        group.alpha = isActive ? 1 : 0;
    }

    public void Upgrade(int upgradeIndex) {
        int level = upgradeLevels[upgradeIndex];
        if (level < upgradeItemUIs[upgradeIndex].Length - 1) {
            upgradeLevels[upgradeIndex]++;
            upgradeItemUIs[upgradeIndex][level + 1].Activate();
            workbenches[upgradeIndex].GetComponent<IActivatable>().Activate(true);
        }
    }
}
