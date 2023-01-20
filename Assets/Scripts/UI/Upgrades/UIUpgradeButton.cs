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



    private void OnEnable() {
        EventBus<UpdateMoneyEvent>.Subscribe(onMoneyUpdate);
    }

    private void OnDisable() {
        EventBus<UpdateMoneyEvent>.Unsubscribe(onMoneyUpdate);
    }

    private void onMoneyUpdate(UpdateMoneyEvent e) {
        evaluateButtonColors(e.money);
    }

    void Start() {

    }

    void Update() {

    }

    private void evaluateButtonColors(int globalMoney) {
        if (tower != null) {
            if (tower.getCurrentUpgradeFromType(upgradeType).getParent() != null) {
                if (tower.getCurrentUpgradeFromType(upgradeType).getParent().getCost() > globalMoney) {
                    upgradeImage.color = new Color(1f, 0, 0, 1f);
                } else {
                    upgradeImage.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
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
