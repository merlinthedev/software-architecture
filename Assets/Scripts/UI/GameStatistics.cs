using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameStatistics : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI scoreText;

    private int healthText;
    private int moneyText;
    private int waveText;

    private GameManager gameManager;

    private void OnEnable() {
        EventBus<UpdateHealthEvent>.Subscribe(onHealthUpdate);
        EventBus<UpdateMoneyEvent>.Subscribe(onMoneyUpdate);
        EventBus<UpdateWaveEvent>.Subscribe(onWaveUpdate);
    }

    private void OnDisable() {
        EventBus<UpdateHealthEvent>.Unsubscribe(onHealthUpdate);
        EventBus<UpdateMoneyEvent>.Unsubscribe(onMoneyUpdate);
        EventBus<UpdateWaveEvent>.Unsubscribe(onWaveUpdate);
    }


    private void Start() {
        // Refactor?
        gameManager = GameManager.getInstance();
        moneyText = gameManager.getMoney();
        healthText = gameManager.getHealth();
        waveText = gameManager.getWaveNumber();
        updateText();
        Debug.Log("Received instance");
    }

    private void onMoneyUpdate(UpdateMoneyEvent e) {
        moneyText = e.money;
        updateText();

    }

    private void onWaveUpdate(UpdateWaveEvent e) {
        waveText = e.wave;
        updateText();
    }

    private void onHealthUpdate(UpdateHealthEvent e) {
        healthText = e.health;
        updateText();
    }

    public void updateText() {

        // scoreText.SetText("Money: " + gameManager.getMoney() +
        //     " Health: " + gameManager.getHealth() +
        //     " Wave: " + gameManager.getWaveNumber());
        // scoreText.SetText("Money: " + amount);
        scoreText.SetText("Money:" + moneyText + " Health:" + healthText + " Wave:" + waveText);
        Debug.Log("Updated text");
    }



}
