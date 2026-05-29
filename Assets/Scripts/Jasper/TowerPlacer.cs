using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    public Tilemap[] placementMaps;
    public Tilemap[] nonPlacementMaps;

    public GameObject ghostPrefab;

    private HashSet<Vector3Int> _occupiedTiles = new HashSet<Vector3Int>();
    private GameObject _ghostInstance;

    private void Update()
    {
        HandlePlacementHover();
        HandlePlacementClick();
    }
    void HandlePlacementHover()
    {
        if (TowerSelectionUI.selectedTowerPrefab == null)
        {
            if (_ghostInstance != null)
            {
                Destroy(_ghostInstance);
                _ghostInstance = null;
            }
            return;
        }

        if (_ghostInstance == null)
        {
            _ghostInstance = Instantiate(ghostPrefab);
        }
        _ghostInstance.GetComponent<SpriteRenderer>().sprite = TowerSelectionUI.selectedTowerPrefab.GetComponent<SpriteRenderer>().sprite;

        Vector3Int cell = CellPosition();
        Vector3 worldCenter = PrimaryPlacementMap().GetCellCenterWorld(cell);
        worldCenter.z = 0;

        _ghostInstance.transform.position = worldCenter + new Vector3(0, PrimaryPlacementMap().cellSize.y * 0.25f);
        _ghostInstance.GetComponent<GhostTower>().SetValid(IsValidPlacement(cell));
    }

    void HandlePlacementClick()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }
        if (TowerSelectionUI.selectedTowerPrefab == null)
        {
            return;
        }
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Vector3Int cell = CellPosition();
        if (!IsValidPlacement(cell))
        {
            return;
        }

        GameObject tower = Instantiate(TowerSelectionUI.selectedTowerPrefab, _ghostInstance.transform.position, Quaternion.identity);

        Tilemap owningMap = GetOwningPlacementMap(cell);
        if (owningMap != null)
        {
            tower.transform.SetParent(owningMap.transform, worldPositionStays: true);

            TilemapRenderer tilemapRenderer = owningMap.GetComponent<TilemapRenderer>();
            if (tilemapRenderer != null)
            {
                SpriteRenderer _spriteRenderer = tower.GetComponent<SpriteRenderer>();
                if (_spriteRenderer != null)
                {
                    _spriteRenderer.sortingLayerID = tilemapRenderer.sortingLayerID;
                    _spriteRenderer.sortingOrder = tilemapRenderer.sortingOrder + 1;
                }
            }
        }

        TowerSelectionUI.selectedTowerPrefab = null;
        _occupiedTiles.Add(cell);
    }
    
    Tilemap GetOwningPlacementMap(Vector3Int cell)
    {
        foreach (var map in placementMaps)
        {
            if (map != null && map.HasTile(cell))
            {
                return map;
            }
        }
        return null;
    }
    Tilemap PrimaryPlacementMap()
    {
        foreach (var map in placementMaps)
        {
            if (map != null)
            {
                return map;
            }
        }
        return null;
    }
    Vector3 MouseWorldPosition()
    {
        Vector3 _mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseWorldPosition.z = 0;
        return _mouseWorldPosition;
    }

    Vector3Int CellPosition()
    {
        return PrimaryPlacementMap().WorldToCell(MouseWorldPosition());
    }    

    bool IsValidPlacement(Vector3Int cell)
    {
        bool hasPlacementTile = false;
        foreach (var map in placementMaps)
        {
            if (map != null && map.HasTile(cell))
            {
                hasPlacementTile = true;
                break;
            }
        }
        if (!hasPlacementTile)
        {
            return false;
        }

        foreach (var map in nonPlacementMaps)
        {
            if (map != null && map.HasTile(cell))
            {
                return false;
            }
        }

        return !_occupiedTiles.Contains(cell);
    }
}
