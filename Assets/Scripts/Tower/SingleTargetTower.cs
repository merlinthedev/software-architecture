using System.Collections;
using System.Collections.Generic;
using System.Numerics;

using UnityEngine;

public class SingleTargetTower : Tower {



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
    private int rangeLevel = -1;
    [SerializeField] private List<Upgrade> attackSpeedUpgrades = new List<Upgrade>();
    private int attackSpeedLevel = -1;
    [SerializeField] private List<Upgrade> damageUpgrades = new List<Upgrade>();
    private int damageLevel = -1;


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



    private void Start() {

        initializeDictionary();

        base.drawCircle(steps, range, lineRenderer, drawHeight);
        base.initialize(targetCollider, range, drawHeight);
        StartCoroutine(attack());
    }

    private void Update() {

    }

    protected override IEnumerator attack() {
        while (true) {
            if (targets.Count > 0) {
                if (targets[0].isAlive()) {
                    targets[0].takeDamage(damage, Enemy.DamageType.FLAT);
                } else {
                    targets.RemoveAt(0);
                    yield return null;
                }
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void initializeDictionary() {
        // Add lists to map
        upgrades.Add("Range", rangeUpgrades);
        upgrades.Add("AS", attackSpeedUpgrades);
        upgrades.Add("Damage", damageUpgrades);
    }


    private void OnTriggerEnter(Collider other) {
        // When target enters the collider, add them to the target list
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

    public int getRangeLevel() {
        return this.rangeLevel;
    }

    public int getAttackSpeedLevel() {
        return this.attackSpeedLevel;
    }

    public int getDamageLevel() {
        return this.damageLevel;
    }

    public override Upgrade getCurrentUpgradeLevel(string upgradeType) {
        if (upgradeType == "Range") {
            if (rangeLevel != -1) {
                return upgrades[upgradeType][rangeLevel];
            } else {
                return null;
            }
        } else if (upgradeType == "AS") {
            if (attackSpeedLevel != -1) {
                return upgrades[upgradeType][attackSpeedLevel];
            } else {
                return null;
            }
        } else if (upgradeType == "Damage") {
            if (damageLevel != -1) {
                return upgrades[upgradeType][damageLevel];
            } else {
                return null;
            }
        } else {
            return null;
        }
    }

}
