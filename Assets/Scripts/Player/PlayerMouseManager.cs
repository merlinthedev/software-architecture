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

            startHover();

            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("Mouse button down");
                placeTower(getTileAtMouse());
            }

            if (Input.GetMouseButtonDown(1)) {
                Debug.Log("Mouse button down");
                Destroy(selectedTower);
                selectedTower = null;
            }

        }


    }

    private Tile getTileAtMouse() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.tag == "Tile") {
                Debug.Log("Tile hit");
                Tile selectedTile = hit.collider.gameObject.GetComponent<Tile>();
                if (!selectedTile.isOccupied()) {
                    Debug.LogWarning("Tile is not occupied");
                    return selectedTile;
                } else {
                    Debug.LogError("This tile is already occupied");
                    return null;
                }
            } 
        }

        return null;
    }

    private void placeTower(Tile tile) {
        if (tile == null) {
            Destroy(selectedTower);
            selectedTower = null;
            isHovering = false;
            return;
        }

        selectedTower.transform.position = tile.transform.position;
        selectedTower.transform.position = new Vector3(selectedTower.transform.position.x, selectedTower.transform.position.y + 0.8f, selectedTower.transform.position.z);

        // Do this but without GetComponent
        GameManager.getInstance().removeMoney(selectedTower.GetComponent<Tower>().Cost);
        Debug.Log("Removed money");


        selectedTower = null;
        isHovering = false;

        tile.setOccupied(true);

    }

    private void startHover() {
        Tile hoverTile = getTileAtMouse();
        if (hoverTile == null) return;
        if (!hoverTile.isOccupied()) {
            hoverTile.transform.localScale = new Vector3(5f, 5f, 5f);
        }
    }

    public void dragTower(GameObject obj) {
        // Do this but without GetComponent
        if (GameManager.getInstance().getMoney() < obj.GetComponent<Tower>().Cost) {
            Debug.LogError("Not enough money");
            return;
        }

        if (!GameManager.getInstance().isBuildingPhase()) {
            Debug.LogError("Cannot build towers during battle phase");
            return;
        }

        Debug.Log("Initiating tower drag mechanic");
        isHovering = true;
        selectedTower = Instantiate(obj);

    }



}
