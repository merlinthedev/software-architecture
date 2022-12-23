using System.Collections.Generic;
using UnityEngine;

public interface IEnemy {
    public float MovementSpeed { get; set; }
    public int Health { get; set; }
    public int Value { get; set; }

}
