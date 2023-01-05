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

    public UnityEvent onGameEnd;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Start() {
        onGameEnd = new UnityEvent();
        //onGameEnd.AddListener()
    }

    private void Update() {
        if (gameOver) {
            //onGameEnd.Invoke(
            //        gameWon ? "You won!" : "You lost!");
            Time.timeScale = 0;
        }
    }

    public void takeGlobalDamage(int damage) {
        if (this.health - damage <= 0) {
            globalDeath();
            setHealth(0);
            return;
        }
        setHealth(this.health -= damage);
    }

    private void globalDeath() {
        EnemyManager.getInstance().getEnemyList().ForEach(enemy => {
            enemy.MovementSpeed = 0;
        });
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
