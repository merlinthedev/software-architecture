using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class Tower : MonoBehaviour {


    protected abstract int Steps { get; set; }
    protected abstract float Range { get; set; }
    protected abstract LineRenderer LineRenderer { get; }
    protected abstract SphereCollider TargetCollider { get; }
    protected abstract float DrawHeight { get; set; }

    protected abstract float Damage { get; set; }
    protected abstract float FireRate { get; set; }

    public abstract int Cost { get; set; }

    protected abstract IEnumerator attack();

    public abstract Upgrade getNextUpgrade(string upgradeType);
    public abstract List<Upgrade> getUpgradeListFromType(string upgradeType);
    public abstract Dictionary<string, List<Upgrade>> getUpgradeMap();
    protected abstract void onTowerPlaced(TowerPlacedEvent e);

    protected virtual void OnEnable() {
        EventBus<TowerPlacedEvent>.Subscribe(onTowerPlaced);
    }

    protected virtual void OnDisable() {
        EventBus<TowerPlacedEvent>.Unsubscribe(onTowerPlaced);
    }


    protected void initialize(SphereCollider targetCollider, float range, float drawHeight) {
        targetCollider.radius = range;
        targetCollider.center = new Vector3(0, drawHeight, 0);
    }

    protected void drawCircle(int steps, float radius, LineRenderer lineRenderer, float drawHeight) {
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
