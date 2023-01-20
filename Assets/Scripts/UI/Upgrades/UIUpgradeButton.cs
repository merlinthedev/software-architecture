using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UIUpgradeButton : MonoBehaviour {

    [SerializeField] private TMPro.TextMeshProUGUI upgradeName;
    [SerializeField] private UnityEngine.UI.Image upgradeImage;

    private string upgradeType;
    private bool isMaxed = false;
    private Tower tower;

    private UIUpgradeController upgradeController;

    void Start() {

    }

    void Update() {

    }

    public void setController(UIUpgradeController controller) {
        this.upgradeController = controller;
    }

    public void setTower(Tower tower) {
        this.tower = tower;
    }

    public void setIsMaxed(bool value) {
        this.isMaxed = value;
    }

    public void setUpgradeType(string upgradeType) {
        this.upgradeType = upgradeType;
    }

    public void setUpgradeText(string text) {
        upgradeName.text = text;
    }

    public void upgradeTower() {
        if (!isMaxed) {
            if (GameManager.enoughMoney(tower.getNextUpgrade(upgradeType).getCost())) {
                EventBus<RemoveMoneyEvent>.Raise(new RemoveMoneyEvent(tower.getNextUpgrade(upgradeType).getCost()));
                EventBus<TowerUpgradeEvent>.Raise(new TowerUpgradeEvent(upgradeType, tower));
            }
        }
        upgradeController.hideUI();
    }

}
