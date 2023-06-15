using UnityEngine;

public class TowerBuildManager : BuildingManager
{
    public TransformArrayData grid;
    [SerializeField] private GameObject _towerPrefab;

    public override void Build()
    {
        PlaceAtGridPoint(1, 1, _towerPrefab);
    }

    private void PlaceAtGridPoint(int x, int y, GameObject prefab)
    {
        Transform gridPoint = grid[x, y];
        _towerPrefab = prefab;

        Instantiate(_towerPrefab, gridPoint.position, Quaternion.identity);
    }
}
