using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class AOETower : MonoBehaviour, ITower {
    [SerializeField] private int steps;
    
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private int _cost;
    [SerializeField] private float _fireRate;

    [SerializeField] private float drawHeight;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private SphereCollider targetCollider;



    public List<Enemy> targets = new List<Enemy>();

    public float range {
        get {
            return _range;
        }
        set {
            _range = value;
        }
    }

    public int damage {
        get {
            return _damage;
        }
        set {
            _damage = value;
        }
    }

    public int cost {
        get {
            return _cost;
        }
        set {
            _cost = value;
        }
    }

    public float fireRate {
        get {
            return _fireRate;
        }
        set {
            _fireRate = value;
        }
    }

    private void Start() {
        StartCoroutine(attack());

        drawCirlce(steps, _range);

        targetCollider.radius = _range;
        targetCollider.center = new Vector3(0, drawHeight, 0);
    }

    private void OnTriggerEnter(Collider other) {
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


    IEnumerator attack() {
        while (true) {
            if (targets.Count > 0) {
                foreach (Enemy enemy in targets.ToList()) {
                    if (enemy.isAlive()) {
                        enemy.takeDamage(_damage);
                    } else {
                        targets.Remove(enemy);
                    }
                }
            }
            yield return new WaitForSeconds(fireRate);
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

