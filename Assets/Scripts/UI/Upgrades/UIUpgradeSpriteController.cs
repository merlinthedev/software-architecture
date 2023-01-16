using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class UIUpgradeSpriteController : MonoBehaviour {
    [SerializeField] private string upgradeType;
    [SerializeField] private UpgradeTextController upgradeTextController;

    private Upgrade upgrade;
    private void OnEnable() {
        EventBus<UpdateMoneyEvent>.Subscribe(onMoneyUpdate);
    }

    private void OnDisable() {
        EventBus<UpdateMoneyEvent>.Unsubscribe(onMoneyUpdate);
    }

    private void onMoneyUpdate(UpdateMoneyEvent e) {
        changeSpriteColors();
    }

    private void changeSpriteColors() {
        if (GameManager.enoughMoney(upgrade.getCost())) {
            GetComponent<Image>().color = Color.white;
        } else {
            GetComponent<Image>().color = Color.red;
        }
    }

    public void setUpgradeText() {
        upgradeTextController.updateText(upgrade.getCost().ToString() + "$" + " \n" + upgrade.getName() + " \n" + upgrade.getMulitplier().ToString() + "x");
    }

    public string getUpgradeType() {
        return this.upgradeType;
    }

    public UpgradeTextController getUpgradeTextController() {
        return this.upgradeTextController;
    }

    public void setUpgrade(Upgrade upgrade) {
        this.upgrade = upgrade;
    }

}
