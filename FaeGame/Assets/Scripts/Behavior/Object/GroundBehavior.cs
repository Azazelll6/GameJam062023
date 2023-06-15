using UnityEngine;

public class GroundBehavior : MonoBehaviour
{
    public Vector2Int gridLocation;
    private TileArrayData _grid;

    public void SetGridCoord(int i, int j, TileArrayData grid)
    {
        gridLocation = new Vector2Int(i, j);
        _grid = grid;
    }

    public Transform GetTransform()
    {
        return _grid[gridLocation.x, gridLocation.y].transform;
    }
}
