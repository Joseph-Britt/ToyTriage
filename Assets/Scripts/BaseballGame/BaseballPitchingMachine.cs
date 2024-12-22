using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballPitchingMachine : MonoBehaviour {

    [SerializeField] private float baseballSpeed = 10f;
    [SerializeField] private float spawnRate = 5f;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject baseballPrefab;

    private float spawnTimer;
    private bool isEnabled;

    private void Update() {
        if (!isEnabled) {
            return;
        }
        if (spawnTimer <= 0) {
            GameObject baseball = Instantiate(baseballPrefab);
            baseball.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            baseball.GetComponent<Baseball>().Throw(baseballSpeed);
            spawnTimer = spawnRate;
        } else {
            spawnTimer -= Time.deltaTime;
        }
    }

    public void SetEnabled(bool isEnabled) {
        this.isEnabled = isEnabled;
        spawnTimer = spawnRate;
    }
}
