using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TowerPlacer : MonoBehaviour
{
    //Tilemaps for placeable and blocked tiles
    public Tilemap[] placementMaps;
    public Tilemap[] nonPlacementMaps;

    //Preview of tower before placement
    public GameObject ghostPrefab;

    //Which cells are occupied by towers (?)
    private HashSet<Vector3Int> _occupiedTiles = new HashSet<Vector3Int>();
    //Current tower preview
    private GameObject _ghostInstance;

    //Reference to points manager for checking money
    private PointsManager pointsManager;

    void Awake()
    {
        //Find the points manager in the scene
        pointsManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PointsManager>();
    }

    private void Update()
    {
        //Update the preview position and validity
        HandlePlacementHover();
        //Handle tower placement input
        HandlePlacementClick();
    }
    /// <summary>
    /// Updates the ghost tower position and appearance while hovering
    /// </summary>
    void HandlePlacementHover()
    {
        //Remove the ghost tower if no tower is selected
        if (TowerSelectionUI.selectedTowerPrefab == null)
        {
            if (_ghostInstance != null)
            {
                Destroy(_ghostInstance);
                _ghostInstance = null;
            }
            return;
        }
        //Create the ghost tower if it doesn't exist
        if (_ghostInstance == null)
        {
            _ghostInstance = Instantiate(ghostPrefab);
        }
        //Match ghost sprite to selected tower sprite
        _ghostInstance.GetComponent<SpriteRenderer>().sprite = TowerSelectionUI.selectedTowerPrefab.GetComponent<SpriteRenderer>().sprite;

        //Get hovered cell position
        Vector3Int cell = CellPosition();
        //Calculate world position of cell center
        Vector3 worldCenter = PrimaryPlacementMap().GetCellCenterWorld(cell);
        worldCenter.z = 0;

        //Position the ghost tower slightly above the cell center for better visibility
        _ghostInstance.transform.position = worldCenter + new Vector3(0, PrimaryPlacementMap().cellSize.y * 0.83f);
        //Set ghost tower colour based on placement validity
        _ghostInstance.GetComponent<GhostTower>().SetValid(IsValidPlacement(cell));
    }

    /// <summary>
    /// Places the tower on click if the placement is valid and the player has enough money, then updates occupied tiles and deducts money
    /// </summary>
    void HandlePlacementClick()
    {
        
        if (!Input.GetMouseButtonDown(0))
        {
            //Only handle left mouse button clicks
            return;
        }
        if (TowerSelectionUI.selectedTowerPrefab == null)
        {
            //No tower selected, so ignore clicks
            Debug.Log("No tower selected for placement.");
            return;
        }
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            //Click is over UI, so ignore it
            Debug.Log("Click is over UI, ignoring.");
            return;
        }

        //Get the cell being clicked on
        Vector3Int cell = CellPosition();

        //Check if placement is valid and player has enough money
        if (!IsValidPlacement(cell) || pointsManager.money < 10)
        {
            //Invalid placement or not enough money, so ignore the click
            Debug.Log("Invalid placement or insufficient funds, cannot place tower.");
            return;
        }

        //Create tower at the ghost tower position (which is already aligned to the cell center)
        GameObject tower = Instantiate(TowerSelectionUI.selectedTowerPrefab, _ghostInstance.transform.position, Quaternion.identity);

        //Find the tilemap that owns the cell and parent the tower to it for proper sorting
        Tilemap owningMap = GetOwningPlacementMap(cell);
        if (owningMap != null)
        {
            tower.transform.SetParent(owningMap.transform, worldPositionStays: true);
            Debug.Log($"Parented tower to tilemap {owningMap.name} for proper sorting.");

            //Get the tilemap renderer to determine sorting layer and order
            TilemapRenderer tilemapRenderer = owningMap.GetComponent<TilemapRenderer>();
            if (tilemapRenderer != null)
            {
                //Get the tower's SpriteRenderer and set its sorting layer and order to be above the tilemap for proper rendering
                Debug.Log($"Found tilemap renderer with sorting layer {tilemapRenderer.sortingLayerID} and order {tilemapRenderer.sortingOrder} for tower placement.");

                SpriteRenderer _spriteRenderer = tower.GetComponent<SpriteRenderer>();
                if (_spriteRenderer != null)
                {
                    //Set the tower's sorting layer and order to be above the tilemap for proper rendering
                    Debug.Log($"Setting tower sorting layer to {tilemapRenderer.sortingLayerID} and order to {tilemapRenderer.sortingOrder + 1}");

                    _spriteRenderer.sortingLayerID = tilemapRenderer.sortingLayerID;
                    _spriteRenderer.sortingOrder = tilemapRenderer.sortingOrder + 1;
                }
            }
        }

        TowerSelectionUI.selectedTowerPrefab = null;
        //Mark the cell as occupied by adding it to the set of occupied tiles, which will prevent future placements on the same cell until it is freed up (e.g. by selling the tower)
        _occupiedTiles.Add(cell);
        //Deduct the tower cost from the player's money
        pointsManager.money -= 10;
    }
    /// <summary>
    /// Returns the placement tilemap that contains the given cell, or null if none do
    /// </summary>
    Tilemap GetOwningPlacementMap(Vector3Int cell)
    {
        //Check each placement map to see if it has a tile at the given cell position, and return the first one that does
        foreach (var map in placementMaps)
        {
            //If the map exists and has a tile at the cell position, return it as the owning map
            if (map != null && map.HasTile(cell))
            {
                return map;
            }
        }
        //No placement map contains the cell, so return null to indicate invalid placement
        return null;
    }
    /// <summary>
    /// Returns the first valid placement tilemap (one that exists and has at least one tile), or null if none are valid.
    /// </summary>
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
    /// <summary>
    /// Gets the mouse position in world coordinates, with the z coordinate set to 0 for 2D placement. This is used to determine which cell the mouse is hovering over for tower placement.
    /// </summary>
    /// <returns>The mouse position in world coordinates with z set to 0.</returns>
    Vector3 MouseWorldPosition()
    {
        Vector3 _mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseWorldPosition.z = 0;
        return _mouseWorldPosition;
    }
    /// <summary>
    /// Converts the mouse world position to a cell position on the tilemap, which is used to determine where to place the tower and whether the placement is valid based on the tilemaps.
    /// </summary>
    Vector3Int CellPosition()
    {
        return PrimaryPlacementMap().WorldToCell(MouseWorldPosition());
    }
    /// <summary>
    /// Checks whether a tower can be placed on the given cell by verifying that it is on a valid placement tile, not on a non-placement tile, and not already occupied by another tower. This ensures that towers can only be placed in designated areas and do not overlap with each other.
    /// </summary>
    bool IsValidPlacement(Vector3Int cell)
    {
        bool hasPlacementTile = false;

        //First check if the cell is on any of the placement tilemaps
        foreach (var map in placementMaps)
        {
            //If the map exists and has a tile at the cell position, mark that we have found a valid placement tile and break out of the loop
            if (map != null && map.HasTile(cell))
            {
                hasPlacementTile = true;
                break;
            }
        }
        //If no placement tile was found on any of the placement maps, then the placement is invalid, so return false
        if (!hasPlacementTile)
        {
            return false;
        }

        //Next check if the cell is on any of the non-placement tilemaps, which would block placement even if there is a valid placement tile
        foreach (var map in nonPlacementMaps)
        {
            //If the map exists and has a tile at the cell position, then placement is blocked, so return false
            if (map != null && map.HasTile(cell))
            {
                return false;
            }
        }

        //Finally, check if the cell is already occupied by another tower, which would also block placement. If the cell is in the set of occupied tiles, then placement is invalid, so return false. Otherwise, return true to indicate that the placement is valid.
        return !_occupiedTiles.Contains(cell);
    }
}
