using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariables : MonoBehaviour {

    [SerializeField] private int health;
    [SerializeField] private int value;




    /* Getters and Setters */ 
    public int getHealth() {
        return this.health;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public int getValue() {
        return this.value;
    }

    public void setValue(int value) {
        this.value = value;
    }

}
