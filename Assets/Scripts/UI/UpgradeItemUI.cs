using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeItemUI : MonoBehaviour {

    public static event EventHandler<EventArgs> OnAnyClicked;

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private float activeAlphaValue = 1f;
    [SerializeField] private float deactiveAlphaValue = 0.5f;

    private UpgradeSO data;
    private CanvasGroup group;
    private bool isActive;
    private UpgradeItemUI previous;

    private void Awake() {
        if (OnAnyClicked == null) {
            OnAnyClicked = new EventHandler<EventArgs>(OnAnyClicked);
        }
        OnAnyClicked += UpgradeItemUI_OnAnyClicked;
        group = GetComponent<CanvasGroup>();
        Deactivate();
    }

    private void UpgradeItemUI_OnAnyClicked(object sender, EventArgs e) {
        ActivateIfPreviousInactive();
    }

    public void Setup(UpgradeSO data, UpgradeItemUI previous) {
        this.previous = previous;
        this.data = data;
        title.text = data.title;
        description.text = "Cost: " + data.cost +
            "\nNice: " + ProbabilityToPercent(data.niceProbability) + "%" +
            "\nSpecial: " + ProbabilityToPercent(data.specialProbability) + "%";
        ActivateIfPreviousInactive();
    }

    private int ProbabilityToPercent(float probability) {
        return (int) (probability * 100);
    }

    public void OnClick() {
        if (ScoreSystem.Instance.TryPurchase(data.cost)) {
            Deactivate();
        }
    }

    private void Activate() {
        button.enabled = true;
        group.alpha = activeAlphaValue;
        isActive = true;
    }

    private void ActivateIfPreviousInactive() {
        if (previous == null || !previous.IsActive()) {
            Activate();
        }
    }

    private void Deactivate() {
        button.enabled = false;
        group.alpha = deactiveAlphaValue;
        isActive = false;
    }

    public bool IsActive() {
        return isActive;
    }

    public UpgradeSO GetData() {
        return data;
    }
}
