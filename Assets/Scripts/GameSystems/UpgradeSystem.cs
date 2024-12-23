using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour {
    
    public static UpgradeSystem Instance { get; private set; }

    public enum UpgradeItem {
        BASEBALL,
        SNOWGLOBE
    }

    private float[] niceProbabilities;
    private float[] specialProbabilities;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There is more than one UpgradeSystem " + Instance);
        }
        niceProbabilities = new float[Enum.GetNames(typeof(UpgradeItem)).Length];
        specialProbabilities = new float[Enum.GetNames(typeof(UpgradeItem)).Length];
    }

    public void SetNiceProbability(UpgradeItem item, float probability) {
        niceProbabilities[(int)item] = probability;
    }

    public float GetNiceProbability(UpgradeItem item) {
        return niceProbabilities[(int)item];
    }

    public void SetSpecialProbability(UpgradeItem item, float probability) {
        specialProbabilities[(int)item] = probability;
    }

    public float GetSpecialProbability(UpgradeItem item) {
        return specialProbabilities[(int)item];
    }
}
