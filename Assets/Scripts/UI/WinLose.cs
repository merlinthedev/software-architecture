using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLose : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI winLoseText;

    private void Start() {
        winLoseText.SetText("");
    }

    private void Update() {
        if (GameManager.getInstance().isGameOver()) {
            if (GameManager.getInstance().isGameWon()) {
                winLoseText.SetText("You won!");
            } else {
                winLoseText.SetText("You lost!");
            }
        }
    }




}
