using UnityEngine;
using UnityEngine.UI;

public class TowerSelectionUI : MonoBehaviour
{
    public static GameObject selectedTowerPrefab;
    public Button purchaseButton;
    public int price = 0;
    public PointsManager moneyManager;

    private void Start()
    {
        moneyManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PointsManager>();
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
            TowerPlacer.price = 0;
            return;
        }
        TowerPlacer.price = price;
        selectedTowerPrefab = towerPrefab;
    }
}
