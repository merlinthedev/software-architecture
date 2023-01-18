using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using UnityEngine;

public class DebuffTower : Tower {

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

    protected override void OnEnable() {
        EventBus<TowerPlacedEvent>.Subscribe(onTowerPlaced);

    }

    protected override void OnDisable() {
        EventBus<TowerPlacedEvent>.Unsubscribe(onTowerPlaced);
    }

    protected override void onTowerPlaced(TowerPlacedEvent e) {
        Debug.LogError("TOWER PLACED EVENT RAISED");
        if (e.tower == this) {
            StartCoroutine(attack());
        }
    }

    void Start() {
        base.initialize(targetCollider, range, drawHeight);
        base.drawCircle(steps, range, lineRenderer, drawHeight);

    }

    void Update() {

    }

    protected override IEnumerator attack() {
        while (true) {
            if (targets.Count > 0) {
                foreach (Enemy enemy in targets.ToList()) {
                    if (enemy.isAlive()) {
                        enemy.takeDamage(damage, Enemy.DamageType.DEBUFF);
                    } else {
                        targets.Remove(enemy);
                        yield return null;
                    }
                }
            }
            yield return new WaitForSeconds(fireRate);
        }
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
                enemy.MovementSpeed = enemy.getBaseMovementSpeed();
                enemy.getAgent().speed = enemy.getBaseMovementSpeed();
                enemy.setDebuffed(false);
                targets.Remove(enemy);
            }
        }
    }

    

    public override Upgrade getNextUpgrade(string upgradeType) {
        throw new System.NotImplementedException();
    }

    public override List<Upgrade> getUpgradeListFromType(string upgradeType) {
        throw new System.NotImplementedException();
    }

    public override Dictionary<string, List<Upgrade>> getUpgradeMap() {
        return upgrades;
    }
}
