using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour, ITower {

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int steps;
    [SerializeField] private float _range;
    [SerializeField] private float _damage;
    [SerializeField] private int _cost;

    public float range {
        get { return _range; }
        set { _range = value; }
    }

    public float damage {
        get { return _damage; }
        set { _damage = value; }
    }

    public int cost {
        get { return _cost; }
        set { _cost = value; }
    }

    private void Start() {
        drawCirlce(steps, range);
    }

    public void attack() {
        throw new System.NotImplementedException();
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
