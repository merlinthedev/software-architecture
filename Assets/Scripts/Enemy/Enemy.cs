using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

using TMPro;

public class Enemy : MonoBehaviour, IEnemy {

    [SerializeField] private float movementSpeed;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int value;

    [SerializeField] private bool alive;
    [SerializeField] private bool debuffed;

    [SerializeField] private Collider enemyCollider;


    [SerializeField] private NavMeshAgent agent;

    private Transform agentDestination;

    private float baseMovementSpeed;


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

    public int MaxHealth {
        get {
            return maxHealth;
        }

        set {
            maxHealth = value;
        }
    }

    public bool Alive {
        get {
            return alive;
        }

        set {
            alive = value;
        }
    }

    public bool Debuffed {
        get {
            return debuffed;
        }

        set {
            debuffed = value;
        }
    }


    #endregion

    private int pointer;

    private void Start() {
        agent.speed = movementSpeed;
        baseMovementSpeed = movementSpeed;
    }

    private void Update() {
        moveEnemy();
    }


    private void moveEnemy() {
        //if (pointer < waypoints.Count) {
        //    transform.position = Vector3.MoveTowards(transform.position, waypoints[pointer].transform.position, movementSpeed * Time.deltaTime);
        //    if ((transform.position - waypoints[pointer].transform.position).magnitude < 0.001) {
        //        //Debug.Log("arrived at destination");
        //        pointer++;
        //    }
        //}

        agent.speed = movementSpeed;
        agent.destination = agentDestination.transform.position;
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
        EventBus<EnemyKilledEvent>.Raise(new EnemyKilledEvent(this));
    }


    public float getBaseMovementSpeed() {
        return this.baseMovementSpeed;
    }

    public NavMeshAgent getAgent() {
        return this.agent;
    }

    public Collider getCollider() {
        return this.enemyCollider;
    }

    public void setDestinationTransform(Transform target) {
        this.agentDestination = target;
    }

    // Enum for damage types
    public enum DamageType {
        FLAT,
        DEBUFF,
    }

}
