using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UISpriteController : MonoBehaviour {

    [SerializeField] private Image spriteImage;
    [SerializeField] private Tower referenceTower;

    private void OnEnable() {
        EventBus<UpdateMoneyEvent>.Subscribe(onMoneyUpdate);
    }

    private void OnDisable() {
        EventBus<UpdateMoneyEvent>.Unsubscribe(onMoneyUpdate);
    }

    private void Start() {
        // Refactor
        spriteColors(GameManager.getInstance().getMoney());
    }

    private void onMoneyUpdate(UpdateMoneyEvent e) {
        spriteColors(e.money);
    }

    private void spriteColors(int money) {
        // Events
        if (money < referenceTower.Cost) {
            spriteImage.color = Color.red;
        } else {
            spriteImage.color = Color.white;
        }
    }


}
