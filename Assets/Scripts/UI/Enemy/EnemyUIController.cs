using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour {
    [SerializeField] private Enemy enemy;
    [SerializeField] private Image healthBarImage;

    private void Update() {
        if (enemy != null) {
            float healthPercentage = (float)enemy.Health / (float)enemy.MaxHealth;
            healthBarImage.rectTransform.sizeDelta = new Vector2(healthPercentage * 4, healthBarImage.rectTransform.sizeDelta.y);
            switch (healthPercentage) {
                case float n when (n <= 0.333f):
                    healthBarImage.color = Color.red;
                    break;
                case float n when (n <= 0.667f):
                    healthBarImage.color = Color.yellow;
                    break;
                case float n when (n <= 1f):
                    healthBarImage.color = Color.green;
                    break;
            }
        }
    }
}
