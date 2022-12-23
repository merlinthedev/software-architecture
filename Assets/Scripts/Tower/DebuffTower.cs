using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private int damage;
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

    protected override int Damage {
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


    protected override IEnumerator attack() {
        throw new System.NotImplementedException();
    }

    void Start() {
        base.initialize(targetCollider, range, drawHeight);
        base.drawCircle(steps, range, lineRenderer, drawHeight);
    }

    void Update() {

    }
}
