using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointManager : MonoBehaviour {

    private GameManager gm;

    private void Start() {
        gm = GameManager.getInstance();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            Enemy hit;
            EnemyManager.getInstance().getEnemyMap().TryGetValue(other, out hit);
            EnemyManager.getInstance().removeFromList(hit);
            EnemyManager.getInstance().removeFromMap(hit.getCollider());
            gm.takeGlobalDamage(hit.Value);
            Destroy(hit.gameObject);
        }
    }

}
