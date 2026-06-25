using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerSelectionUI : MonoBehaviour
{
    public static GameObject selectedTowerPrefab;
    public Button purchaseButton;
    public int price = 0;
    public PointsManager moneyManager;
    public PhysicsRaycaster camCast;

    private void Start()
    {
        moneyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PointsManager>();
        camCast = Camera.main.GetComponent<PhysicsRaycaster>();

    }
    private void Update()
    {
        if (moneyManager.money >= price && purchaseButton.interactable == false)
        {
            purchaseButton.interactable = true;
        }
        else if (moneyManager.money < price && purchaseButton.interactable == true)
        {
            purchaseButton.interactable = false;

        }

        //if (moneyManager.money >= price)
        //{
        //    purchaseButton.interactable = true;
        //}
        //else 
        //{
        //    purchaseButton.interactable = false;

        //}
    }
    public void SelectedTower(GameObject towerPrefab)
    {
        if (towerPrefab == selectedTowerPrefab)
        {

            selectedTowerPrefab = null;
            camCast.enabled = true;
            TowerPlacer.price = 0;
            return;
        }
        TowerPlacer.price = price;
        selectedTowerPrefab = towerPrefab;
        camCast.enabled = false;

    }
}
