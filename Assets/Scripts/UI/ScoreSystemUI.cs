using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystemUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private string displayPrefix;

    private void Start() {
        ScoreSystem.Instance.OnCurrencyChanged += ScoreSystem_OnCurrencyChanged;
    }

    private void ScoreSystem_OnCurrencyChanged(object sender, System.EventArgs e) {
        currencyText.text = displayPrefix + ScoreSystem.Instance.GetCurrency();
    }
}
