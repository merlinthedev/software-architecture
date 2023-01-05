using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteController : MonoBehaviour {

    [SerializeField] private Image spriteImage;
    [SerializeField] private Tower referenceTower;
    
    private void Start() {
            
    }

    private void spriteColors() {
        if (GameManager.getInstance().getMoney() < referenceTower.Cost) {
            spriteImage.color = Color.red;
        } else {
            spriteImage.color = Color.white;
        }
    }

    
}
