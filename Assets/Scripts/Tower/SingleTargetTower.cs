using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetTower : MonoBehaviour, ITower {

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private SphereCollider targetCollider;
    [SerializeField] private int steps;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private int _cost;
    [SerializeField] private float _fireRate;
    [SerializeField] private float drawHeight;

    public List<Enemy> targets;


    public float range {
        get { return _range; }
        set { _range = value; }
    }

    public int damage {
        get { return _damage; }
        set { _damage = value; }
    }

    public int cost {
        get { return _cost; }
        set { _cost = value; }
    }

    public float fireRate {
        get { return _fireRate; }
        set {
            _fireRate = value;
        }
    }



    private void Start() {
        drawCirlce(steps, range);

        targets = new List<Enemy>();
        targetCollider.radius = _range;
        targetCollider.center = new Vector3(0, drawHeight, 0);

        StartCoroutine(attack());
    }

    private void Update() {
    }

    IEnumerator attack() {
        while (true) {
            if (targets.Count > 0) {
                if (targets[0].isAlive()) {
                    targets[0].takeDamage(_damage);
                } else {
                    targets.RemoveAt(0);
                }
            }
            yield return new WaitForSeconds(_fireRate);
        }
    }


    private void OnTriggerEnter(Collider other) {
        // When target enters the collider, add them to the target list
        if (other.CompareTag("Enemy")) {
            var enemy = EnemyManager.instance.getEnemyFromMap(other);
            targets.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy")) {
            var enemy = EnemyManager.instance.getEnemyFromMap(other);
            if (targets.Contains(enemy)) {
                targets.Remove(enemy);
            }
        }
    }

    public void drawCirlce(int steps, float radius) {
        lineRenderer.positionCount = steps + 1;
        lineRenderer.useWorldSpace = false;
        float x;
        float y = drawHeight;
        float z;

        float angle = 20f;

        for (int i = 0; i < (steps + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / steps);
        }
    }

}
