using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Data/ControllerData/TileData")]
public class TileData : ScriptableObject
{
    public GameObject environmentPrefab, occupiedPrefab;
    [HideInInspector] public Transform transform;
    public int priority;
}
