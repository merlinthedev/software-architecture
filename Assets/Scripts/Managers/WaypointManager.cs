using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour {
    [SerializeField] private List<GameObject> waypointList;

    private void Start() {
        Debug.Log("Amount of waypoints in list: " + waypointList.Count);
    }


    public List<GameObject> getWaypointList() {
        Debug.Log("Passing waypoint list with " + waypointList.Count + " waypoints");
        return waypointList;
    }

}
