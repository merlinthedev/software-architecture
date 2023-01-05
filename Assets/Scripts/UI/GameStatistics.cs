using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameStatistics : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;

    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.getInstance();
    }

    private void Update() {
        if (gameManager.isUpdated()) {
            scoreText.SetText("Money: " + gameManager.getMoney() +
                " \nHealth: " + gameManager.getHealth() +
                " \nWave: " + gameManager.getWaveNumber());
        }
    }



}
