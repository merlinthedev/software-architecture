using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class WavePauseTextManager : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI wavePauseText;


    private void OnEnable() {
        EventBus<WavePauseEvent>.Subscribe(onWavePause);
    }

    private void OnDisable() {
        EventBus<WavePauseEvent>.Unsubscribe(onWavePause);
    }

    void Start() {

    }

    void Update() {

    }

    private void onWavePause(WavePauseEvent e) {
        wavePauseText.text = e.isPaused ? "Next wave will start in " + e.timeLeft + " seconds." : "";
    } 
}
