using System.Collections.Generic;

using UnityEngine;

public interface IEnemy {
    public float MovementSpeed { get; set; }
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public int Value { get; set; }

    public bool Alive { get; set; }
    public bool Debuffed { get; set; }

}
