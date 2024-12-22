using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystemUI : MonoBehaviour {

    [SerializeField] Transform rowContainer;
    [SerializeField] Transform rowTemplate;
    [SerializeField] Transform itemTemplate;
    [SerializeField] List<UpgradeGroupSO> upgrades;

    private void Start() {
        foreach (UpgradeGroupSO upgradeGroup in upgrades) {
            Transform row = Instantiate(rowTemplate);
            row.SetParent(rowContainer);
            row.gameObject.SetActive(true);
            UpgradeItemUI previous = null;
            foreach (UpgradeSO upgradeItem in upgradeGroup.upgrades) {
                Transform item = Instantiate(itemTemplate);
                item.SetParent(row);
                item.gameObject.SetActive(true);
                if (item.TryGetComponent(out UpgradeItemUI upgradeUI)) {
                    upgradeUI.Setup(upgradeItem, previous);
                }
            }
        }
    }
}
