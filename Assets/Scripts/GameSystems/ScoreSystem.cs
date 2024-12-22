using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour {
    
    public static ScoreSystem Instance { get; private set; }

    public event EventHandler<EventArgs> OnScoreChanged;
    public event EventHandler<EventArgs> OnCurrencyChanged;

    private int score;
    private int currency;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogError("There is more than one ScoreSystem " + Instance);
        }
        OnScoreChanged = new EventHandler<EventArgs>(OnScoreChanged);
        OnCurrencyChanged = new EventHandler<EventArgs>(OnCurrencyChanged);
    }

    public void Reward(int score) {
        this.score += score;
        OnScoreChanged?.Invoke(this, EventArgs.Empty);
        currency += score;
        OnCurrencyChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool TryPurchase(int cost) {
        if (CanAfford(cost)) {
            Purchase(cost);
            return true;
        }
        return false;
    }

    public void Purchase(int cost) {
        currency -= cost;
        OnCurrencyChanged.Invoke(this, EventArgs.Empty);
    }

    public bool CanAfford(int cost) {
        return currency >= cost;
    }

    public int GetScore() {
        return score;
    }

    public int GetCurrency() {
        return currency;
    }
}
