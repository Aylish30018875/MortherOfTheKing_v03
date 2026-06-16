using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    public static GameObject selectedTowerPrefab;
    public int price = 0;

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
