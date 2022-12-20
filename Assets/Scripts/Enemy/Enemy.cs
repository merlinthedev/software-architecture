using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy {

    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _health;
    [SerializeField] private int _value;

    [SerializeField] private bool alive = true;

    [SerializeField] private Collider _collider;


    private Vector3 lastPosition;
    private float distanceTraveled;

    public float movementSpeed {
        get {
            return _movementSpeed;
        }
        set {
            _movementSpeed = value;
        }
    }

    public int health {
        get {
            return _health;
        }

        set {
            _health = value;
        }
    }

    public int value {
        get {
            return _value;
        }

        set {
            _value = value;
        }
    }

    private WaypointManager waypointManager;
    private List<GameObject> waypoints;
    private int pointer;

    private void Start() {
        if (waypointManager == null) {
            Debug.LogError("waypoint error");
        } else {
            waypoints = waypointManager.getWaypointList();
            Debug.Log("Waypoints initialized");
        }
    }

    private void Update() {
        moveEnemy();
        trackDistance();
    }

    private void trackDistance() {
        distanceTraveled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        //Debug.Log("Total distance traveled: " + distanceTraveled);
    }

    private void moveEnemy() {
        if (pointer < waypoints.Count) {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[pointer].transform.position, _movementSpeed * Time.deltaTime);
            if ((transform.position - waypoints[pointer].transform.position).magnitude < 0.001) {
                Debug.Log("arrived at destination");
                pointer++;
            }
        }
    }



    public void takeDamage(int damage) {
        if (this.health - damage <= 0) {
            // enemy needs to die
            //Debug.LogError("Enemy has died however, you have not implemented this method yet :)");
            die();
            return;
        }
        this.health -= damage;
        Debug.Log("Enemy health after taking damage: " + this.health);

    }

    private void die() {
        this.alive = false;
        //GameManager.instance.addMoney(_value);
        //EnemyManager.instance.removeFromList(this);
        //EnemyManager.instance.removeFromMap(_collider);

        Destroy(this.gameObject);
    }

    public float getDistanceTraveled() {
        return this.distanceTraveled;
    }

    public void setWaypointManager(WaypointManager wp) {
        this.waypointManager = wp;
    }

    public Collider getCollider() {
        return this._collider;
    }

    public bool isAlive() {
        return this.alive;
    }

}
