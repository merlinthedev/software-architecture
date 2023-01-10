using Unity.VisualScripting.Dependencies.NCalc;

using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    [SerializeField] private int health;
    [SerializeField] private int money = 0;
    [SerializeField] private int waveNumber;

    [SerializeField] private bool gameOver = false;
    [SerializeField] private bool gameWon = false;
    [SerializeField] private bool buildingPhase = true;


    private static GameManager instance;


    private void OnEnable() {
        EventBus<GlobalDamageEvent>.Subscribe(onGlobalDamage);
        EventBus<EnemyKilledEvent>.Subscribe(onEnemyKilled);
        EventBus<GameIsWonEvent>.Subscribe(onGameWon);
    }

    private void OnDisable() {
        EventBus<GlobalDamageEvent>.Unsubscribe(onGlobalDamage);
        EventBus<EnemyKilledEvent>.Unsubscribe(onEnemyKilled);
        EventBus<GameIsWonEvent>.Unsubscribe(onGameWon);
    }


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
    }

    private void Update() {
        if (gameOver) {
            // Do text stuff
            Time.timeScale = 0;
        }
    }

    private void takeGlobalDamage(int damage) {
        setHealth(this.health -= damage);

        if (this.health <= 0) {
            globalDeath();
            setHealth(0);
        }

        EventBus<UpdateHealthEvent>.Raise(new UpdateHealthEvent(this.getHealth()));
    }

    private void onGameWon(GameIsWonEvent e) {
        gameOver = true;
        gameWon = e.isWon;

    }


    private void onGlobalDamage(GlobalDamageEvent e) {
        Debug.Log("Global Damage: " + e.enemy.Value);
        takeGlobalDamage(e.enemy.Value);
    }

    private void onEnemyKilled(EnemyKilledEvent e) {
        addMoney(e.enemy.Value);
        EventBus<UpdateMoneyEvent>.Raise(new UpdateMoneyEvent(this.getMoney()));
    }

    private void globalDeath() {
        EventBus<GameIsOverEvent>.Raise(new GameIsOverEvent(true));
        gameOver = true;
    }

    public int getMoney() {
        return this.money;
    }

    public void addMoney(int value) {
        this.money += value;
    }

    public void removeMoney(int value) {
        this.money -= value;
    }
    public int getWaveNumber() {
        return this.waveNumber;
    }
    public void setWave(int value) {
        this.waveNumber = value;
    }

    public void setHealth(int value) {
        this.health = value;
    }

    public int getHealth() {
        return this.health;
    }

    public static GameManager getInstance() {
        return instance;
    }

    public bool isBuildingPhase() {
        return this.buildingPhase;
    }

    public void setBuildingPhase(bool value) {
        this.buildingPhase = value;
    }

    public bool isGameOver() {
        return this.gameOver;
    }

    public void setGameWon(bool value) {
        this.gameOver = true;
        this.gameWon = value;
    }

    public bool isGameWon() {
        return this.gameWon;
    }

}
