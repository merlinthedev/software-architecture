using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMouseManager : MonoBehaviour {


    [SerializeField] private Camera mainCamera;


    private GameObject selectedTower = null;

    private bool isHovering = false;

    private void Start() {
        Debug.Log("Camera main script start");
    }


    private void Update() {
        if (isHovering && selectedTower != null) {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 50f;
            selectedTower.transform.position = mainCamera.ScreenToWorldPoint(mousePosition);

            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("Mouse button down");
                getTileAtMouse();
            }

        }


    }

    private void getTileAtMouse() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.tag == "Tile") {
                Debug.Log("Tile hit");
                placeTower(hit.transform);
            } else {
                Debug.Log("Instead of tile we hit: " + hit.collider.name);
            }
        }
    }

    private void placeTower(Transform tilePosition) {
        selectedTower.transform.position = tilePosition.position;
        selectedTower.transform.position = new Vector3(selectedTower.transform.position.x, selectedTower.transform.position.y + 0.8f, selectedTower.transform.position.z);
        selectedTower = null;
        isHovering = false;

    }

    public void dragTower(GameObject obj) {
        Debug.Log("Initiating tower drag mechanic");
        isHovering = true;
        selectedTower = Instantiate(obj);

    }



}
