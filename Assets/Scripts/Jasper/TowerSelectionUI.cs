using UnityEngine;

public class TowerSelectionUI : MonoBehaviour
{
    public static GameObject selectedTowerPrefab;

    public void SelectedTower(GameObject towerPrefab)
    {
        if (towerPrefab == selectedTowerPrefab)
        {
            selectedTowerPrefab = null;
            return;
        }
        selectedTowerPrefab = towerPrefab;
    }
}
