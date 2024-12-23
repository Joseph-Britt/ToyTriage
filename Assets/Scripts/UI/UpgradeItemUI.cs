using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour {

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private float activeAlphaValue = 1f;
    [SerializeField] private float deactiveAlphaValue = 0.5f;
    [SerializeField] private CanvasGroup group;

    private UpgradeSO data;
    private int upgradeIndex;

    private void Awake() {
        Deactivate();
    }

    public void Setup(UpgradeSO data, int upgradeIndex) {
        this.data = data;
        this.upgradeIndex = upgradeIndex;
        title.text = data.title;
        description.text = "Cost: " + data.cost +
            "\nNice: " + ProbabilityToPercent(data.niceProbability) + "%" +
            "\nSpecial: " + ProbabilityToPercent(data.specialProbability) + "%";
    }

    private int ProbabilityToPercent(float probability) {
        return (int) (probability * 100);
    }

    public void OnClick() {
        if (ScoreSystem.Instance.TryPurchase(data.cost)) {
            Deactivate();
            UpgradeSystemUI.Instance.Upgrade(upgradeIndex);
            UpgradeSystem.Instance.SetNiceProbability(data.item, data.niceProbability);
            UpgradeSystem.Instance.SetSpecialProbability(data.item, data.specialProbability);
        }
    }

    public void Activate() {
        button.enabled = true;
        group.alpha = activeAlphaValue;
    }

    public void Deactivate() {
        button.enabled = false;
        group.alpha = deactiveAlphaValue;
    }

    public UpgradeSO GetData() {
        return data;
    }
}
