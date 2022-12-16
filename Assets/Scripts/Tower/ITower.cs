using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower {
    public float range { get; set; }
    public int damage { get; set; }

    public int cost { get; set; }

    public float fireRate { get; set; }

    public void attack();
    public void drawCirlce(int steps, float radius);



}
