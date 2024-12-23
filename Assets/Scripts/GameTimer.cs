using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour {

    [SerializeField] private float gameTime = 180;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float timer;

    private void Awake() {
        timer = gameTime;
    }

    private void Update() {
        timer -= Time.deltaTime;
        timerText.text = "" + (int)timer;
        if (timer <= 0) {
            Player.Instance.StartInteraction(null);
            scoreText.text = "Score: " + ScoreSystem.Instance.GetScore();
            gameOver.SetActive(true);
        }
    }
}
