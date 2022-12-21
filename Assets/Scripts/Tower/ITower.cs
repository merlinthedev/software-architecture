using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITower {
    float range { get; set; }
    int damage { get; set; }

    int cost { get; set; }

    float fireRate { get; set; }

    void drawCirlce(int steps, float radius);



}
