using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [Header("Spawning")]
    [SerializeField] private float spawnRate;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform spawnerPosition;
    [SerializeField] private int maxEnemies;
    [SerializeField] private int enemiesSpawned;


    [SerializeField] private WaypointManager wp;


    private List<Enemy> enemyList;
    private Dictionary<Collider, Enemy> enemyMap = new Dictionary<Collider, Enemy>();

    private static EnemyManager instance;

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start() {
        enemyList = new List<Enemy>();

        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy() {
        while (enemiesSpawned < maxEnemies) {
            Enemy returned = Instantiate(enemy, spawnerPosition.position, Quaternion.identity);
            enemyMap.Add(returned.getCollider(), returned);
            enemyList.Add(returned);
            returned.setWaypointList(wp.getWaypointList());
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnRate);
        }

    }


    public void removeFromList(Enemy enemy) {
        this.enemyList.Remove(enemy);
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
