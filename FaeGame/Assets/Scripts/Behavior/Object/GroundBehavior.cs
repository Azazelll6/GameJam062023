using UnityEditor;
using UnityEngine;


public class GroundBehavior : MonoBehaviour, IDrawGizmo
{
    public Vector2Int gridLocation;
    public TileData tileData;
    private TileArrayData _grid;

    private void Start()
    {
        gridLocation = tileData.gridCoord;
    }

    public void SetGrid(TileArrayData grid)
    {
        _grid = grid;
    }
    
    public Vector2Int GetGridLocation()
    {
        return gridLocation;
    }

    public TileData GetGridObj()
    {
        return _grid[gridLocation];
    }

    public void OnDrawGizmos()
    {
        if (tileData == null)
        {
            Debug.LogError("tileData is null");
            return;
        }

        if (tileData.transform == null)
        {
            Debug.LogError("tileData.transform is null");
            return;
        }
#if UNITY_EDITOR
        if (Selection.Contains(gameObject))
        {
            Gizmos.color = Color.red;
            Vector3 position = transform.position;
            Gizmos.DrawWireCube(position, transform.localScale);
        
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.fontSize = 15;
            
            // Draw the text at the top of the cube.
            position.y += transform.localScale.y/-2f - 0.5f; // Add some offset.
        
            // You'll need the UnityEditor namespace for Handles
            Handles.Label(position, gridLocation.x + ", " + gridLocation.y);

        }
#endif
    }
}
