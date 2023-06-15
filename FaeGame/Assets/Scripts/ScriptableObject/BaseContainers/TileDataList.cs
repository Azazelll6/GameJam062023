using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileDataList", menuName = "Data/List/TileDataList")]
public class TileDataList : ScriptableObject
{
    public List<TileData> tileDataList;

    public int Size()
    {
        return tileDataList.Count;
    }

    public int GetPriority()
    {
        var priority = 0;
        foreach (TileData tileData in tileDataList)
        {
            priority += tileData.priority;
        }
        return priority;
    }

    public GameObject GetRandomPrefab()
    {
        GameObject prefab = tileDataList[Random.Range(0, Size())].environmentPrefab;
        return prefab;
    }

    public GameObject GetOccupiedPrefab()
    {
        GameObject prefab = tileDataList[0].occupiedPrefab;
        return prefab;
    }
}