using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private int health;
    private int money;
    private int waveNumber;

    public static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void takeGlobalDamage(int damage) {
        if (this.health - damage <= 0) {
            Debug.LogError("Global player has died pls implement this function");
            this.health = 0;
            return;
        }
        this.health -= damage;
    }
}
