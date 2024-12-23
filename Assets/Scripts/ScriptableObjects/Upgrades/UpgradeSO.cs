using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeSO : ScriptableObject {

    public string title;
    public int cost;
    public UpgradeSystem.UpgradeItem item;
    public float niceProbability;
    public float specialProbability;
}
