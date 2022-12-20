using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private List<Enemy> enemyList;
    private Dictionary<Collider, Enemy> enemyMap = new Dictionary<Collider, Enemy>();

    public static EnemyManager instance;

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start() {
        enemyList = new List<Enemy>();
    }

    public void addToList(Enemy enemy) {
        enemyList.Add(enemy);
    }

    public List<Enemy> getEnemyList() {
        return this.enemyList;
    }

    public Dictionary<Collider, Enemy> getEnemyMap() {
        return this.enemyMap;
    }
}
