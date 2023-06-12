using UnityEngine;

public class Grid2D : MonoBehaviour
{
    public enum CellType { Empty, Building };
    private CellType[,] grid;

    public void Grid(int width, int height)
    {
        grid = new CellType[width, height];
    }

    public void PlaceBuilding(int x, int y)
    {
        grid[x, y] = CellType.Building;
    }
}
