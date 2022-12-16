using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
    public float movementSpeed { get; set; }
    public int health { get; set; }
    public int value { get; set; }

}
