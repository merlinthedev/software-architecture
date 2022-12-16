using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour, ITower {

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private SphereCollider targetCollider;
    [SerializeField] private int steps;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private int _cost;
    [SerializeField] private float _fireRate;

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
    }

    private void Update() {

        if (targets.Count > 0 && targets != null) {
            InvokeRepeating("attack", 0, 5);
        }

    }

    public void attack() {
        targets[0].takeDamage(_damage);
    }


    private void OnTriggerEnter(Collider other) {
        // When target enters the collider, add them to the target list
        if (other.CompareTag("Enemy")) {
            var enemy = other.GetComponent<Enemy>();
            targets.Add(enemy);
            Debug.Log("Enemy has been added to the list. (WITH GETCOMPONENT, PLS FIX =D)");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Enemy")) {
            var enemy = other.GetComponent<Enemy>();
            if (targets.Contains(enemy)) {
                targets.Remove(enemy);
                Debug.Log("Enemy has been removed from the list. (WITH GETCOMPONENT, PLS FIX =D)");
            }
        }
    }

    public void drawCirlce(int steps, float radius) {
        lineRenderer.positionCount = steps + 1;
        lineRenderer.useWorldSpace = false;
        float x;
        float y = -5f;
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
