using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower {
    public float range { get; set; }
    public float damage { get; set; }

    public int cost { get; set; }

    public void attack();
    public void drawCirlce(int steps, float radius);



}
