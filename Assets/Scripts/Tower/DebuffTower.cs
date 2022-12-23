using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    protected override int Cost {
        get {
            return cost;
        }
        set {
            cost = value;
        }
    }

    #endregion

    private List<Enemy> targets = new List<Enemy>();


    void Start() {
        base.initialize(targetCollider, range, drawHeight);
        base.drawCircle(steps, range, lineRenderer, drawHeight);

        StartCoroutine(attack());
    }

    void Update() {

    }

    protected override IEnumerator attack() {
        while (true) {
            if (targets.Count > 0) {
                foreach (Enemy enemy in targets.ToList()) {
                    if (enemy.isAlive()) {
                        Debug.Log("Enemy is taking damage from debuff tower");
                        enemy.takeDamage(damage, Enemy.DamageType.DEBUFF);
                    } else {
                        targets.Remove(enemy);
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
                targets.Remove(enemy);
            }
        }
    }
}
