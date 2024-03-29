﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.VisualScripting;

using UnityEngine;

class AOETower : Tower {
    [Header("Range indication")]
    [SerializeField] private int steps;
    [SerializeField] private float range;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private SphereCollider targetCollider;
    [SerializeField] private float drawHeight;

    [Header("Tower statistics")]
    [SerializeField] private float fireRate;
    [SerializeField] private float damage;
    [SerializeField] private int cost;

    [Header("Upgrades")]
    [SerializeField] private List<Upgrade> rangeUpgrades = new List<Upgrade>();
    private int rangeLevel = 0;
    [SerializeField] private List<Upgrade> attackSpeedUpgrades = new List<Upgrade>();
    private int attackSpeedLevel = 0;
    [SerializeField] private List<Upgrade> damageUpgrades = new List<Upgrade>();
    private int damageLevel = 0;




    private Dictionary<string, List<Upgrade>> upgrades = new Dictionary<string, List<Upgrade>>();




    private List<Enemy> targets = new List<Enemy>();

    #region protected
    protected override int Steps {
        get {
            return steps;
        }
        set {
            steps = value;
        }
    }


    protected override float Range {
        get {
            return range;
        }
        set {
            range = value;
        }
    }

    protected override LineRenderer LineRenderer {
        get {
            return lineRenderer;
        }
    }

    protected override SphereCollider TargetCollider {
        get {
            return targetCollider;
        }
    }

    protected override float DrawHeight {
        get {
            return drawHeight;
        }
        set {
            drawHeight = value;
        }
    }

    protected override float Damage {
        get {
            return damage;
        }
        set {
            damage = value;
        }
    }

    protected override float FireRate {
        get {
            return fireRate;
        }
        set {
            fireRate = value;
        }
    }

    public override int Cost {
        get {
            return cost;
        }
        set {
            cost = value;
        }
    }




    #endregion

    protected override void OnEnable() {
        EventBus<TowerPlacedEvent>.Subscribe(onTowerPlaced);
        EventBus<TowerUpgradeEvent>.Subscribe(onTowerUpgrade);

    }

    protected override void OnDisable() {
        EventBus<TowerPlacedEvent>.Unsubscribe(onTowerPlaced);
        EventBus<TowerUpgradeEvent>.Unsubscribe(onTowerUpgrade);
    }

    protected override void onTowerPlaced(TowerPlacedEvent e) {
        if (e.tower == this) {
            StartCoroutine(attack());
        }
    }

    private void Start() {

        initializeDictionary();

        base.drawCircle(steps, range, lineRenderer, drawHeight);
        base.initialize(targetCollider, range, drawHeight);

    }

    private void initializeDictionary() {
        upgrades.Add("Range", rangeUpgrades);
        upgrades.Add("AS", attackSpeedUpgrades);
        upgrades.Add("Damage", damageUpgrades);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            var enemy = EnemyManager.getInstance().getEnemyFromMap(other);
            targets.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy")) {
            var enemy = EnemyManager.getInstance().getEnemyFromMap(other);
            if (targets.Contains(enemy)) {
                targets.Remove(enemy);
            }
        }
    }


    protected override IEnumerator attack() {
        while (true) {
            if (targets.Count > 0) {
                foreach (Enemy enemy in targets.ToList()) {
                    if (enemy.Alive) {
                        enemy.takeDamage(damage, Enemy.DamageType.FLAT);
                    } else {
                        targets.Remove(enemy);
                        yield return null;
                    }
                }
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void onTowerUpgrade(TowerUpgradeEvent e) {
        if (e.tower == this) {
            switch (e.upgradeType) {
                case "Range":
                    onRangeUpgrade();
                    break;
                case "AS":
                    onAttackSpeedUpgrade();
                    break;
                case "Damage":
                    onDamageUpgrade();
                    break;
                default:
                    Debug.LogError("Invalid upgrade type: " + e.upgradeType + " for tower: " + this.name);
                    break;
            }
        }
    }

    private void onRangeUpgrade() {
        rangeLevel++;
        range *= upgrades["Range"][rangeLevel].getMultiplier();

        base.initialize(targetCollider, range, drawHeight);
        base.drawCircle(steps, range, lineRenderer, drawHeight);

    }

    private void onDamageUpgrade() {
        damageLevel++;
        damage *= upgrades["Damage"][damageLevel].getMultiplier();
    }

    private void onAttackSpeedUpgrade() {
        attackSpeedLevel++;
        fireRate *= upgrades["AS"][attackSpeedLevel].getMultiplier();
    }


    public override int getRangeLevel() {
        return this.rangeLevel;
    }

    public override int getAttackSpeedLevel() {
        return this.attackSpeedLevel;
    }

    public override int getDamageLevel() {
        return this.damageLevel;
    }

    public override int getUpgradeLevelFromType(string upgradeType) {
        switch (upgradeType) {
            case "Range":
                return rangeLevel;
            case "AS":
                return attackSpeedLevel;
            case "Damage":
                return damageLevel;
            default:
                Debug.LogError("Invalid upgrade type: " + upgradeType + " for tower: " + this.name);
                return -1;
        }
    }

    public override Upgrade getCurrentUpgradeFromType(string upgradeType) {
        switch (upgradeType) {
            case "Range":
                return upgrades["Range"][rangeLevel];
            case "AS":
                return upgrades["AS"][attackSpeedLevel];
            case "Damage":
                return upgrades["Damage"][damageLevel];
            default:
                Debug.LogError("Invalid upgrade type: " + upgradeType + " for tower: " + this.name);
                return null;
        }
    }

    public override Upgrade getNextUpgrade(string upgradeType) {
        switch (upgradeType) {
            case "Range":
                return upgrades["Range"][rangeLevel + 1];
            case "AS":
                return upgrades["AS"][attackSpeedLevel + 1];
            case "Damage":
                return upgrades["Damage"][damageLevel + 1];
            default:
                Debug.LogError("Invalid upgrade type: " + upgradeType + " for tower: " + this.name);
                return null;
        }
    }

    public override List<Upgrade> getUpgradeListFromType(string upgradeType) {
        if (upgrades.ContainsKey(upgradeType)) {
            return upgrades[upgradeType];
        } else {
            Debug.LogError("Invalid upgrade type: " + upgradeType + " for tower: " + this.name);
            return null;
        }
    }

    public override Dictionary<string, List<Upgrade>> getUpgradeMap() {
        return upgrades;
    }

}

