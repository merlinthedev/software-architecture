using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLose : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI winLoseText;

    private void OnEnable() {
        EventBus<GameIsOverEvent>.Subscribe(onGameOver);
        EventBus<GameIsWonEvent>.Subscribe(onGameWon);
    }

    private void OnDisable() {
        EventBus<GameIsOverEvent>.Unsubscribe(onGameOver);
        EventBus<GameIsWonEvent>.Unsubscribe(onGameWon);
    }

    private void Start() {
        winLoseText.SetText("");
    }

    private void onGameOver(GameIsOverEvent e) {
        if(e.isGameOver) {
            winLoseText.SetText("You lost!");
        }
    }

    private void onGameWon(GameIsWonEvent e) {
        if(e.isWon) {
            winLoseText.SetText("You won!");
        }
    }


}
