using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerTextController : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Tower referenceTower;

    private void Start() {
        costText.SetText(referenceTower.Cost.ToString());
    }
}
