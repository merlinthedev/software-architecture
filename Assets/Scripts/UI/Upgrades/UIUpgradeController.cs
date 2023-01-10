using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UIUpgradeController : MonoBehaviour {

    [SerializeField] private RectTransform rTransform;


    private void OnEnable() {
        EventBus<TowerSelectedEvent>.Subscribe(onTowerSelect);
        EventBus<TowerUnselectEvent>.Subscribe(onTowerUnselect);
    }

    private void OnDisable() {
        EventBus<TowerSelectedEvent>.Unsubscribe(onTowerSelect);
        EventBus<TowerUnselectEvent>.Unsubscribe(onTowerUnselect);
    }

    private void Start() {
        resetUI();
    }

    private void onTowerSelect(TowerSelectedEvent e) {
        rTransform.transform.position = new Vector3(125, rTransform.transform.position.y, rTransform.transform.position.z);

    }

    private void onTowerUnselect(TowerUnselectEvent e) {
        if(e.isUnselected) {
            resetUI();
        }
    }

    private void resetUI() {
        rTransform.transform.position = new Vector3(-125, rTransform.transform.position.y, rTransform.transform.position.z);

    }

}
