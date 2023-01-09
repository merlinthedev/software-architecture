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
        Debug.Log("Received instance");
    }

    public void updateText(int amount) {
        Debug.Log("Updated text" + amount);

        // scoreText.SetText("Money: " + gameManager.getMoney() +
        //     " Health: " + gameManager.getHealth() +
        //     " Wave: " + gameManager.getWaveNumber());
        // scoreText.SetText("Money: " + amount);
    }



}
