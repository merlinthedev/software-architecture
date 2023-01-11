using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrades")]
public class Upgrade : ScriptableObject {

    [SerializeField] private new string name;
    [SerializeField] private string id;
    [SerializeField] private string description;
    [SerializeField] private int cost;
    [SerializeField] private float multiplier;
    [SerializeField] private Upgrade parent;

    public int getCost() { return this.cost; }
    public float getMulitplier() { return this.multiplier; }

}
