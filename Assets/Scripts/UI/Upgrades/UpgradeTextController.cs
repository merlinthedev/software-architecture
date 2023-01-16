using TMPro;

using UnityEngine;

public class UpgradeTextController : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Tower referenceTower;

    public void updateText(string text) {
        costText.text = text;
    }


}