using System.Collections;
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
        if (e.tower == this) {
            StartCoroutine(attack());
        }
    }

    private void Start() {

        base.drawCircle(steps, range, lineRenderer, drawHeight);
        base.initialize(targetCollider, range, drawHeight);

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
                    if (enemy.isAlive()) {
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

    public override Upgrade getNextUpgrade(string upgradeType) {
        throw new System.NotImplementedException();
    }

}

