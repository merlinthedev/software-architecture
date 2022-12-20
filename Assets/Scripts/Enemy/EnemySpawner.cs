using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private float spawnRate;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Transform spawnerPosition;
    [SerializeField] private WaypointManager wp;

    private void Start() {
        InvokeRepeating("spawnEnemy", 0, spawnRate);
    }

    private void Update() {

    }

    private void spawnEnemy() {
        Enemy returned = Instantiate(enemy, spawnerPosition.position, Quaternion.identity);
        EnemyManager.instance.getEnemyMap().Add(returned.getCollider(), returned);
        EnemyManager.instance.addToList(returned);
        returned.setWaypointManager(wp);

    }

}
