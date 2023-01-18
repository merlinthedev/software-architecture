using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UIUpgradeButton : MonoBehaviour {

    [SerializeField] private string upgradeType;

    private Tower tower;

    void Start() {

    }

    void Update() {

    }

    public void setTower(Tower tower) {
        this.tower = tower;
    }

    public void setUpgradeType(string upgradeType) {
        this.upgradeType = upgradeType;
    }
}
