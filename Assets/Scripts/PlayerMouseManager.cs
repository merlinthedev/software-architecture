using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseManager : MonoBehaviour {


    [SerializeField] private Camera mainCamera;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits) {
                if (hit.collider.gameObject.tag == "Tile") {
                    Debug.Log("Tile hit");
                    // Handle click
                }
            }
        }
    }

    void openGUI() {
        // Open GUI
    }
}
