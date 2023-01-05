using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField] private bool occupied;


    public bool isOccupied() {
        return this.occupied;
    }

    public void setOccupied(bool value) { 
        this.occupied = value;
    }
}
