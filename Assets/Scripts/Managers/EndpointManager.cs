using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointManager : MonoBehaviour {

    private GameManager gm;

    private void Start() {
        gm = GameManager.instance;
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            Enemy hit;
            EnemyManager.instance.getEnemyMap().TryGetValue(other, out hit);
            gm.takeGlobalDamage(hit.value);
            Destroy(hit.gameObject);
        }
    }

}
