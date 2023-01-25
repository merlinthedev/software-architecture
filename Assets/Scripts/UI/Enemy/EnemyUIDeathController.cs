using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

public class EnemyUIDeathController : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI enemyDeathText;
    [SerializeField] private RectTransform enemyDeathTextTransform;

    
    private void OnEnable() {
        EventBus<EnemyKilledEvent>.Subscribe(onEnemyKilled);
    }

    private void OnDisable() {
        EventBus<EnemyKilledEvent>.Unsubscribe(onEnemyKilled);
    }

    private void onEnemyKilled(EnemyKilledEvent e) {
        enemyDeathText.text = e.enemy.Value.ToString();
        Debug.LogWarning(e.enemy.Value);
        enemyDeathTextTransform.position = Camera.main.WorldToScreenPoint(e.enemy.transform.position);

        StartCoroutine(fadeOut());

        
    }

    IEnumerator fadeOut() {
        float time = 0;
        while (time < 1) {
            time += Time.deltaTime;
            enemyDeathText.color = new Color(enemyDeathText.color.r, enemyDeathText.color.g, enemyDeathText.color.b, Mathf.Lerp(1, 0, time));
            yield return null;
        }
    }

}
