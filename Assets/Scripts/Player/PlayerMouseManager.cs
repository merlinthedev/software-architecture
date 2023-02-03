using UnityEngine;

public class PlayerMouseManager : MonoBehaviour {


    [SerializeField] private Camera mainCamera;


    private GameObject towerToDrag = null;
    private Tower towerScript = null;
    private GameObject selectedTower = null;

    private bool isHovering = false;

    private void OnEnable() {
        EventBus<TowerUnselectEvent>.Subscribe(onTowerUnselect);
    }

    private void OnDisable() {
        EventBus<TowerUnselectEvent>.Unsubscribe(onTowerUnselect);
    }

    private void Start() { }


    private void Update() {
        if (isHovering && towerToDrag != null) {

            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 50f;
            towerToDrag.transform.position = mainCamera.ScreenToWorldPoint(mousePosition);

            startHover();

            if (Input.GetMouseButtonDown(0)) {
                // Debug.Log("Mouse button down");
                placeTower(getTileAtMouse());
            }

            if (Input.GetMouseButtonDown(1)) {
                // Debug.Log("Mouse button down");
                Destroy(towerToDrag);
                towerToDrag = null;
            }

        } else {
            if (Input.GetMouseButtonDown(0)) {
                clickTower();

                // Pass tower to UI element to display the tower's upgrades

            }
        }


    }

    private Tile getTileAtMouse() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        // use for loop for better performance
        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.tag == "Tile") {
                // Debug.Log("Tile hit");
                Tile selectedTile = hit.collider.gameObject.GetComponent<Tile>();
                return selectedTile.isOccupied() ? null : selectedTile;
            }
        }

        return null;
    }

    private void placeTower(Tile tile) {
        if (tile == null) {
            Destroy(towerToDrag);
            towerToDrag = null;
            isHovering = false;
            return;
        }

        towerToDrag.transform.position = tile.transform.position;
        towerToDrag.transform.position = new Vector3(towerToDrag.transform.position.x, towerToDrag.transform.position.y + 0.8f, towerToDrag.transform.position.z);

        EventBus<TowerPlacedEvent>.Raise(new TowerPlacedEvent(towerScript));
        EventBus<TowerSelectedEvent>.Raise(new TowerSelectedEvent(towerScript));

        // Do this but without GetComponent 
        // Also do this but with events?
        // GameManager.getInstance().removeMoney(towerToDrag.GetComponent<Tower>().Cost);

        EventBus<RemoveMoneyEvent>.Raise(new RemoveMoneyEvent(towerScript.Cost));

        // Debug.Log("Removed money");


        towerToDrag = null;
        towerScript = null;
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
        towerToDrag = Instantiate(obj);
        towerScript = towerToDrag.GetComponent<Tower>();


    }

    private void onTowerUnselect(TowerUnselectEvent e) {
        selectedTower = null;
    }


    private void clickTower() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        // use for loop for better performance
        foreach (RaycastHit hit in hits) {
            if (hit.collider.GetType() == typeof(BoxCollider) && hit.collider.gameObject.CompareTag("Tower")) {
                Debug.Log("Tower hit");
                selectedTower = hit.collider.gameObject;
                EventBus<TowerSelectedEvent>.Raise(new TowerSelectedEvent(selectedTower.GetComponent<Tower>()));
                return;
            }
        }


    }

}
