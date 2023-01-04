using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int money = 0;
    [SerializeField] private int waveNumber;

    [SerializeField] private bool gameOver = false;

    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void takeGlobalDamage(int damage)
    {
        if (this.health - damage <= 0)
        {
            globalDeath();
            this.health = 0;
            return;
        }
        this.health -= damage;
    }

    private void globalDeath()
    {
        EnemyManager.getInstance().getEnemyList().ForEach(enemy =>
        {
            enemy.MovementSpeed = 0;
            gameOver = false;
        });
    }

    public void addMoney(int value)
    {
        this.money += value;
    }

    public void setWave(int value)
    {
        this.waveNumber = value;
    }

    public static GameManager getInstance()
    {
        return instance;
    }

    public bool isGameOver()
    {
        return gameOver;
    }

    public void setGameOver(bool value)
    {
        gameOver = value;
    }


}
