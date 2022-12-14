using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour {
    [SerializeField] private WaypointManager waypointManager;
    [SerializeField] private float speed = 4f;

    private List<GameObject> waypoints = new List<GameObject>();
    private int pointer = 0;



    private void Start() {
        waypoints = waypointManager.getWaypointList();
    }

    private void Update() {
        if (pointer < waypoints.Count) {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[pointer].transform.position, speed * Time.deltaTime);
            if ((transform.position - waypoints[pointer].transform.position).magnitude < 0.001) {
                Debug.Log("arrived at destination");
                pointer++;
            }
        }
    }

}
