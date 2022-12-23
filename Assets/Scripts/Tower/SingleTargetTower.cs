using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetTower : Tower {

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private SphereCollider targetCollider;
    [SerializeField] private int steps;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private int cost;
    [SerializeField] private float fireRate;
    [SerializeField] private float drawHeight;

    public List<Enemy> targets;


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



    private void Start() {
        base.drawCircle(steps, range, lineRenderer, drawHeight);
        base.initialize(targetCollider, range, drawHeight);

        targets = new List<Enemy>();
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
                }
            }
            yield return new WaitForSeconds(fireRate);
        }
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



}
