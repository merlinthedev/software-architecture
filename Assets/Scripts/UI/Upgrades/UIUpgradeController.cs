using System.Collections;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

public class UIUpgradeController : MonoBehaviour {

    private Tower tower;

    [SerializeField] private UIUpgradeButton upgradePrefab;
    [SerializeField] private RectTransform upgradeContainer;

    private List<UIUpgradeButton> upgradeButtons = new List<UIUpgradeButton>();


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
            upgradeButtons.Add(returned);
        }

        for (int i = 0; i < upgradeButtons.Count; i++) {
            upgradeButtons[i].setTower(tower);
            upgradeButtons[i].setUpgradeType(tower.getUpgradeMap().Keys.ToList()[i]);
        }

    }

    private void Start() {
        hideUI();
    }

    public void hideUI() {
        upgradeContainer.position = new Vector3(-125, upgradeContainer.position.y, upgradeContainer.position.z);
        for(int i = 0; i < upgradeButtons.Count; i++) {
            Destroy(upgradeButtons[i]);
        }
    }
}
