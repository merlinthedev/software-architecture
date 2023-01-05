using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndpointManager : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            Enemy hit;
            EnemyManager.getInstance().getEnemyMap().TryGetValue(other, out hit);
            EnemyManager.getInstance().removeFromList(hit);
            EnemyManager.getInstance().removeFromMap(hit.getCollider());
            GameManager.getInstance().takeGlobalDamage(hit.Value);
            Destroy(hit.gameObject);

        }
    }

}
