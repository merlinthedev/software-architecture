using System.Collections;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

public class UIUpgradeController : MonoBehaviour {

    private Tower tower;

    [SerializeField] private UIUpgradeButton upgradePrefab;
    [SerializeField] private RectTransform upgradeContainer;

    [SerializeField] private List<UIUpgradeButton> upgradeButtons = new List<UIUpgradeButton>();


    private void OnEnable() {
        EventBus<TowerSelectedEvent>.Subscribe(onTowerSelected);
    }

    private void OnDisable() {
        EventBus<TowerSelectedEvent>.Unsubscribe(onTowerSelected);
    }

    private void onTowerSelected(TowerSelectedEvent e) {
        tower = e.tower;


        upgradeContainer.position = new Vector3(125, upgradeContainer.position.y, upgradeContainer.position.z);


        foreach (KeyValuePair<string, List<Upgrade>> entry in tower.getUpgradeMap()) {
            var returned = Instantiate(upgradePrefab, transform);
            if (tower.getUpgradeLevelFromType(entry.Key) >= entry.Value.Count - 1) {
                returned.setUpgradeText(entry.Key + " MAX");
                returned.setIsMaxed(true);
            } else {
                returned.setUpgradeText(entry.Key + " " + entry.Value[tower.getUpgradeLevelFromType(entry.Key) + 1].getCost());
            }
            returned.setUpgradeType(entry.Key);
            Debug.LogWarning("Upgrade type: " + entry.Key);
            returned.setTower(tower);
            returned.setController(this);
            upgradeButtons.Add(returned);
        }


    }



    private void Start() {
        hideUI();
    }

    public void hideUI() {
        upgradeContainer.position = new Vector3(-125, upgradeContainer.position.y, upgradeContainer.position.z);
        for (int i = 0; i < upgradeButtons.Count; i++) {
            Destroy(upgradeButtons[i].gameObject);
        }

        upgradeButtons.Clear();
    }
}
