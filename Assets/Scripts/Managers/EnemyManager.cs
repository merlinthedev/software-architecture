using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [Header("Spawning")]
    [SerializeField] private int waveDelay;
    [SerializeField] private float spawnRate;
    [SerializeField] private Transform spawnerPosition;
    [SerializeField] private Transform endpointTransform;
    [SerializeField] private bool shouldSpawn = true;





    [SerializeField] private List<Wave> waveContents;

    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private Dictionary<Collider, Enemy> enemyMap = new Dictionary<Collider, Enemy>();

    private static EnemyManager instance;


    private void OnEnable() {
        EventBus<GlobalDamageEvent>.Subscribe(onGlobalDamage);
        EventBus<EnemyKilledEvent>.Subscribe(onEnemyKilled);
        EventBus<GameIsOverEvent>.Subscribe(onGameOver);
    }

    private void OnDisable() {
        EventBus<GlobalDamageEvent>.Unsubscribe(onGlobalDamage);
        EventBus<EnemyKilledEvent>.Unsubscribe(onEnemyKilled);
        EventBus<GameIsOverEvent>.Unsubscribe(onGameOver);

    }

    private void Awake() {
        if (instance == null) instance = this;
    }

    private void Start() {
        enemyList = new List<Enemy>();

        // Wait 10 seconds before starting to spawn enemies

        if (shouldSpawn) {
            startSpawn();
        }
    }

    private void startSpawn() {
        StartCoroutine(spawnEnemy());
    }

    IEnumerator spawnEnemy() {
        while (!GameManager.getInstance().isGameOver()) {

            for (int i = waveDelay; i >= 0; i--) {
                EventBus<WavePauseEvent>.Raise(new WavePauseEvent(true, i));
                yield return new WaitForSeconds(1);
            }

            EventBus<WavePauseEvent>.Raise(new WavePauseEvent(false, 0));



            foreach (Wave wave in waveContents) {
                // GameManager.getInstance().setBuildingPhase(false);
                GameManager.getInstance().setWave(waveContents.IndexOf(wave) + 1);

                if (spawnRate > 0.4f) {
                    spawnRate -= 0.15f;
                }

                EventBus<UpdateWaveEvent>.Raise(new UpdateWaveEvent(GameManager.getInstance().getWaveNumber()));
                foreach (Enemy enemy in wave.enemies) {
                    Enemy returned = Instantiate(enemy, spawnerPosition.position, Quaternion.identity);
                    // Increase enemy heatlh based on wave number
                    returned.Health += Mathf.Clamp((waveContents.IndexOf(wave) * 20), 10, 200);
                    returned.MaxHealth = returned.Health;
                    Debug.Log("Enemy health: " + returned.Health);
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
                    // GameManager.getInstance().setGameWon(true);
                    EventBus<GameIsWonEvent>.Raise(new GameIsWonEvent(true));
                    yield break;
                }

                Debug.LogWarning("All enemies have been destroyed, starting " + waveDelay + " second cooldown");
                // GameManager.getInstance().setBuildingPhase(true);

                EventBus<WavePauseEvent>.Raise(new WavePauseEvent(true, waveDelay));

                for (int i = waveDelay; i >= 1; i--) {
                    Debug.LogWarning("Cooldown: " + i);
                    EventBus<WavePauseEvent>.Raise(new WavePauseEvent(true, i));
                    yield return new WaitForSeconds(1);
                }

                EventBus<WavePauseEvent>.Raise(new WavePauseEvent(false, 0));

            }
        }

    }

    private void onGlobalDamage(GlobalDamageEvent e) {
        fullEnemyRemove(e.enemy.getCollider());
    }

    private void onEnemyKilled(EnemyKilledEvent e) {
        fullEnemyRemove(e.enemy.getCollider());
    }

    private void onGameOver(GameIsOverEvent e) {
        if (e.isGameOver) {
            for (int i = 0; i < enemyList.Count; i++) {
                enemyList[i].MovementSpeed = 0;
            }
        }
    }


    // public Dictionary<Collider, Enemy> getEnemyMap() {
    //     return this.enemyMap;
    // }

    // public List<Enemy> getEnemyList() {
    //     return this.enemyList;

    // }


    public Enemy getEnemyFromMap(Collider collider) {
        return this.enemyMap[collider];
    }

    private void removeFromMap(Collider collider) {
        this.enemyMap.Remove(collider);
    }

    private void removeFromList(Enemy enemy) {
        this.enemyList.Remove(enemy);
    }

    private void fullEnemyRemove(Collider collider) {
        // Debug.Log("Attempting to remove enemy from list and map with collider: " + collider.ToString());
        removeFromList(getEnemyFromMap(collider));
        removeFromMap(collider);

        Destroy(collider.gameObject);

        // Debug.Log("Successfully removed enemy from list and map");
    }

    public static EnemyManager getInstance() {
        return instance;
    }
}
