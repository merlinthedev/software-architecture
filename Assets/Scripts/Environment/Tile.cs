using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] private bool occupied;

    private void Update() {
        if (transform.localScale != new Vector3(4f, 4f, 4f)) {
            transform.localScale = new Vector3(4f, 4f, 4f);
        }
    }

    public bool isOccupied() {
        return this.occupied;
    }

    public void setOccupied(bool value) {
        this.occupied = value;
    }
}
