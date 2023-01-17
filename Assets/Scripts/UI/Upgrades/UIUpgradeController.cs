using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIUpgradeController : MonoBehaviour {

    [Header("Transform")]
    [SerializeField] private RectTransform rTransform;


    [SerializeField] private List<UIUpgradeSpriteController> sprites = new List<UIUpgradeSpriteController>();


    private Tower tower;


    private void OnEnable() {
        EventBus<TowerSelectedEvent>.Subscribe(onTowerSelect);
    }

    private void OnDisable() {
        EventBus<TowerSelectedEvent>.Unsubscribe(onTowerSelect);
    }

    private void Start() {
        resetUI();
    }

    private void onTowerSelect(TowerSelectedEvent e) {
        rTransform.transform.position = new Vector3(125, rTransform.transform.position.y, rTransform.transform.position.z);
        tower = e.tower;

        passUpgradeData();
    }

    private void passUpgradeData() {
        // Get all 3 types of upgrades and send one to each sprite element
        for (int i = 0; i < sprites.Count; i++) {
            if(tower.getNextUpgrade(sprites[i].getUpgradeType()) == null) {
                                
            }
            sprites[i].setUpgrade(tower.getNextUpgrade(sprites[i].getUpgradeType()));
            sprites[i].setUpgradeText();
        }
    }

    private void resetUI() {
        rTransform.transform.position = new Vector3(-125, rTransform.transform.position.y, rTransform.transform.position.z);
    }

    public void rangeUpgrade() {
        if (GameManager.enoughMoney(tower.getNextUpgrade("Range").getCost())) {
            removeMoney(tower.getNextUpgrade("Range").getCost());
            EventBus<TowerUpgradeEvent>.Raise(new TowerUpgradeEvent("Range", tower));
        }

    }

    public void attackSpeedUpgrade() {
        if (GameManager.enoughMoney(tower.getNextUpgrade("AS").getCost())) {
            removeMoney(tower.getNextUpgrade("AS").getCost());
            EventBus<TowerUpgradeEvent>.Raise(new TowerUpgradeEvent("AS", tower));
        }
    }

    public void damageUpgrade() {
        if (GameManager.enoughMoney(tower.getNextUpgrade("Damage").getCost())) {
            removeMoney(tower.getNextUpgrade("Damage").getCost());
            EventBus<TowerUpgradeEvent>.Raise(new TowerUpgradeEvent("Damage", tower));
        }
    }

    private void removeMoney(int value) {
        EventBus<RemoveMoneyEvent>.Raise(new RemoveMoneyEvent(value));
    }

    public void closeMenu() {
        resetUI();
        tower = null;
        EventBus<TowerUnselectEvent>.Raise(new TowerUnselectEvent(true));
    }

}
