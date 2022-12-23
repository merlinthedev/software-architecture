using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    private List<Enemy> enemyList;
    private Dictionary<Collider, Enemy> enemyMap = new Dictionary<Collider, Enemy>();

    private static EnemyManager instance;

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start() {
        enemyList = new List<Enemy>();
    }

    public void addToList(Enemy enemy) {
        enemyList.Add(enemy);
    }

    public void removeFromList(Enemy enemy) {
        this.enemyList.Remove(enemy);
    }

    public List<Enemy> getEnemyList() {
        return this.enemyList;
    }

    public Dictionary<Collider, Enemy> getEnemyMap() {
        return this.enemyMap;
    }

    public Enemy getEnemyFromMap(Collider collider) {
        return this.enemyMap[collider];
    }

    public void removeFromMap(Collider collider) {
        this.enemyMap.Remove(collider);
    }

    public static EnemyManager getInstance() {
        return instance;
    }
}
