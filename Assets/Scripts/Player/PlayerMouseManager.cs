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
    private Tile selectedTile = null;

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
                selectedTile = hit.collider.gameObject.GetComponent<Tile>();
                if (selectedTile.isOccupied()) {
                    Debug.LogWarning("This tile is already occupied");
                    return;
                }
                placeTower(hit.transform, selectedTile);
            } else {
                Debug.Log("Instead of tile we hit: " + hit.collider.name);
            }
        }
    }

    private void placeTower(Transform tilePosition, Tile tile) {
        selectedTower.transform.position = tilePosition.position;
        selectedTower.transform.position = new Vector3(selectedTower.transform.position.x, selectedTower.transform.position.y + 0.8f, selectedTower.transform.position.z);

        // Do this but without GetComponent
        GameManager.getInstance().removeMoney(selectedTower.GetComponent<Tower>().Cost);
        Debug.Log("Removed money");


        selectedTower = null;
        isHovering = false;
        tile.setOccupied(true);

    }

    public void dragTower(GameObject obj) {
        // Do this but without GetComponent
        if (GameManager.getInstance().getMoney() < obj.GetComponent<Tower>().Cost) {
            Debug.Log("Not enough money");
            return;
        }

        Debug.Log("Initiating tower drag mechanic");
        isHovering = true;
        selectedTower = Instantiate(obj);

    }



}
