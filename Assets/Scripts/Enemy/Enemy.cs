using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IEnemy {

    [SerializeField] private float movementSpeed;
    [SerializeField] private int health;
    [SerializeField] private int value;

    [SerializeField] private bool alive = true;
    [SerializeField] private bool debuffed = false;

    [SerializeField] private Collider enemyCollider;


    [SerializeField] private NavMeshAgent agent;
    private Transform endpointTransform;


    private Vector3 lastPosition;
    private float distanceTraveled;


    #region Properties
    public float MovementSpeed {
        get {
            return movementSpeed;
        }
        set {
            movementSpeed = value;
        }
    }

    public int Health {
        get {
            return health;
        }

        set {
            health = value;
        }
    }

    public int Value {
        get {
            return value;
        }

        set {
            this.value = value;
        }
    }
    #endregion

    private List<GameObject> waypoints;
    private int pointer;

    private void Start() {
        agent.speed = movementSpeed;
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
        //if (pointer < waypoints.Count) {
        //    transform.position = Vector3.MoveTowards(transform.position, waypoints[pointer].transform.position, movementSpeed * Time.deltaTime);
        //    if ((transform.position - waypoints[pointer].transform.position).magnitude < 0.001) {
        //        //Debug.Log("arrived at destination");
        //        pointer++;
        //    }
        //}

        agent.destination = endpointTransform.transform.position;
    }


    /// <summary>
    /// Have the enemy take damage, if the DamageType is debuff, the enemy's movement speed will be multiplied by the damage
    /// Example: if the damage is 0.5, the enemy's movement speed will be halved
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="damageType"></param>
    public void takeDamage(float damage, DamageType damageType) {
        switch (damageType) {
            case DamageType.FLAT:
                if (this.Health - (int)damage <= 0) {
                    // enemy needs to die
                    //Debug.LogError("Enemy has died however, you have not implemented this method yet :)");
                    die();
                    return;
                }
                this.Health -= (int)damage;
                Debug.Log("Enemy health after taking damage: " + this.Health);
                break;
            case DamageType.DEBUFF:
                if (!debuffed) {
                    debuffed = true;
                    this.movementSpeed *= damage;
                    Debug.Log("movement speed has been decreased.");
                }
                break;
        }


    }

    private void die() {
        this.alive = false;

        // Global wallet object?
        GameManager.getInstance().addMoney(value);

        // Events for removing from data structures?
        EnemyManager.getInstance().removeFromList(this);
        EnemyManager.getInstance().removeFromMap(enemyCollider);

        Destroy(this.gameObject);
    }

    public float getDistanceTraveled() {
        return this.distanceTraveled;
    }

    public void setWaypointList(List<GameObject> waypoints) {
        this.waypoints = waypoints;
    }

    public Collider getCollider() {
        return this.enemyCollider;
    }

    public bool isAlive() {
        return this.alive;
    }

    public bool isDebuffed() {
        return this.debuffed;
    }

    public void setDestinationTransform(Transform target) {
        this.endpointTransform = target;
    }

    // Enum for damage types
    public enum DamageType {
        FLAT,
        DEBUFF,
    }

}
