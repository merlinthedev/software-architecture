using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private int health;
    [SerializeField] private int money = 0;
    [SerializeField] private int waveNumber;

    [SerializeField] private bool gameOver = false;


    private static GameManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    private void Update() {
        
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
        gameOver = false;
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

    public bool isGameOver() {
        return gameOver;
    }

    public void setGameOver(bool value) {
        gameOver = value;
    }

    




}
