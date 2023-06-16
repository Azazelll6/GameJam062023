using UnityEngine;

public class TowerBuildManager : BuildingManager
{
    public GameAction TriggerNavRebuild;
    public TileArrayData grid;
    [SerializeField] private GameObject _towerPrefab;
    private UIInterface _uiInterface;
    
    private void OnEnable()
    {
        _uiInterface = FindObjectOfType<UIInterface>();
        if (_uiInterface != null)
        {
            _uiInterface.SendClickDataToTower += OnClickDataReceived;
        }
    }

    private void OnDisable()
    {
        _uiInterface.SendClickDataToTower -= OnClickDataReceived;
    }

    private void OnClickDataReceived(ClickData data)
    {
        Vector2Int gridLocation = data.gridLocation;
        Debug.Log($"{gridLocation} -- {grid.Array2D[gridLocation.x, gridLocation.y].transform.position}");

        PlaceAtGridPoint(gridLocation.x, gridLocation.y, _towerPrefab);
    }


    public override void Build()
    {
        PlaceAtGridPoint(1, 1, _towerPrefab);
    }

    private void PlaceAtGridPoint(int x, int y, GameObject prefab)
    {
        TileData data = grid[x, y];

        Instantiate(prefab, data.transform.position, Quaternion.identity);
    }
}
