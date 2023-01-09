using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [Header("Spawning")]
    [SerializeField] private int waveDelay;
    [SerializeField] private int spawnRate;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform spawnerPosition;
    [SerializeField] private Transform endpointTransform;
    [SerializeField] private bool shouldSpawn = true;




    public List<Wave> waveContents;

    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private Dictionary<Collider, Enemy> enemyMap = new Dictionary<Collider, Enemy>();

    private static EnemyManager instance;


    private void OnEnable() {
        EventBus<GlobalDamageEvent>.Subscribe(onGlobalDamage);
    }

    private void OnDisable() {
        EventBus<GlobalDamageEvent>.Unsubscribe(onGlobalDamage);
    }

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start() {
        enemyList = new List<Enemy>();

        // Wait 10 seconds before starting to spawn enemies

        if (shouldSpawn) Invoke("startSpawn", waveDelay);
    }

    private void startSpawn() {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy() {
        while (!GameManager.getInstance().isGameOver()) {
            foreach (Wave wave in waveContents) {
                GameManager.getInstance().setBuildingPhase(false);
                Debug.LogWarning("Wave: " + (waveContents.IndexOf(wave) + 1));
                GameManager.getInstance().setWave(waveContents.IndexOf(wave) + 1);
                foreach (Enemy enemy in wave.enemies) {
                    Enemy returned = Instantiate(enemy, spawnerPosition.position, Quaternion.identity);
                    enemyMap.Add(returned.getCollider(), returned);
                    enemyList.Add(returned);
                    returned.setDestinationTransform(endpointTransform);
                    yield return new WaitForSeconds(spawnRate);
                }

                // Wait for enemyList to be empty then start cooldown
                while (enemyList.Count > 0) {
                    yield return null;
                }

                if (waveContents.IndexOf(wave) + 1 == waveContents.Count) {
                    GameManager.getInstance().setGameWon(true);
                    yield break;
                }

                Debug.LogWarning("All enemies have been destroyed, starting " + waveDelay + " second cooldown");
                GameManager.getInstance().setBuildingPhase(true);

                yield return new WaitForSeconds(waveDelay);

            }
        }

    }

    private void onGlobalDamage(GlobalDamageEvent e) {
        fullEnemyRemove(e.enemy.getCollider());
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

    private void fullEnemyRemove(Collider collider) {
        // Debug.Log("Attempting to remove enemy from list and map with collider: " + collider.ToString());
        removeFromList(getEnemyFromMap(collider));
        removeFromMap(collider);

        Destroy(collider.gameObject);

        // Debug.Log("Successfully removed enemy from list and map");
    }
}
