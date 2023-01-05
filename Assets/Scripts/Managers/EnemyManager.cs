using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [Header("Spawning")]
    [SerializeField] private float spawnRate;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform spawnerPosition;
    [SerializeField] private int maxEnemies;
    [SerializeField] private int enemiesSpawned;
    [SerializeField] private bool shouldSpawn = true;


    [SerializeField] private Transform endpointTransform;

    [SerializeField] private int waveDelay;

    public List<Wave> waveContents;

    [SerializeField] private List<Enemy> enemyList;
    private Dictionary<Collider, Enemy> enemyMap = new Dictionary<Collider, Enemy>();

    private static EnemyManager instance;

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start() {
        enemyList = new List<Enemy>();


        if (shouldSpawn) StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy() {
        while (!GameManager.getInstance().isGameOver()) {
            foreach (Wave wave in waveContents) {
                Debug.LogWarning("Wave: " + (waveContents.IndexOf(wave) + 1));
                GameManager.getInstance().setWave(waveContents.IndexOf(wave) + 1);
                // enemiesSpawned = 0;
                foreach (Enemy enemy in wave.enemies) {
                    Enemy returned = Instantiate(enemy, spawnerPosition.position, Quaternion.identity);
                    enemyMap.Add(returned.getCollider(), returned);
                    enemyList.Add(returned);
                    returned.setDestinationTransform(endpointTransform);
                    // enemiesSpawned++;
                    yield return new WaitForSeconds(spawnRate);
                }

                // Wait for enemyList to be empty then start cooldown
                while (enemyList.Count > 0) {
                    yield return null;
                }

                Debug.LogWarning("All enemies have been destroyed, starting " + waveDelay + " second cooldown");

                yield return new WaitForSeconds(waveDelay);



            }
        }

    }


    public void removeFromList(Enemy enemy) {
        this.enemyList.Remove(enemy);
    }

    public Dictionary<Collider, Enemy> getEnemyMap() {
        return this.enemyMap;
    }

    public List<Enemy> getEnemyList() {
        return this.enemyList;

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
