using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class UIUpgradeSpriteController : MonoBehaviour {

    private Tower referenceTower;

    private List<Image> upgradeButtons = new List<Image>();
    private void OnEnable() {
        EventBus<UpdateMoneyEvent>.Subscribe(onMoneyUpdate);    
    }

    private void OnDisable() {
        EventBus<UpdateMoneyEvent>.Unsubscribe(onMoneyUpdate);
    }

    private void onMoneyUpdate(UpdateMoneyEvent e) {
        changeSpriteColors();
    }

    private void changeSpriteColors() {
        for(int i = 0; i < upgradeButtons.Count; i++) {
                        
        }
    }


}
