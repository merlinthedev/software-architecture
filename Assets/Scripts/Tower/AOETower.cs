using UnityEngine;

class AOETower : MonoBehaviour, ITower {
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private int _cost;
    [SerializeField] private float _fireRate;

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

    

    public void drawCirlce(int steps, float radius) {
        //How is usiong an interface better for this? I need pre written methods for the classes.
    }
}

