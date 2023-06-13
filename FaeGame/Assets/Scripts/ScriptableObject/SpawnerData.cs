using UnityEngine;

[CreateAssetMenu (fileName = "SpawnerData", menuName = "Data/ManagerData/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    public int creepsAliveCount;
    public IntData globalCreepsAliveCount;
    public CreepData creep;
    public GameObjectList creepPrefabs;

    public void ResetSpawner()
    {
        creepsAliveCount = 0;
    }

    public int GetAliveCount()
    {
        return creepsAliveCount;
    }
    
    public void IncrementCreepsAliveCount()
    {
        creep.IncrementSpawnedTotal();
        globalCreepsAliveCount.UpdateValue(1);
        creepsAliveCount += 1;
    }

    public void CreepKilled()
    {
        creep.IncrementKilledTotal();
        DecrementCreepsAliveCount();
    }

    public void CreepEscaped()
    {
        creep.IncrementEscapedTotal();
        DecrementCreepsAliveCount();
    }

    private void DecrementCreepsAliveCount()
    {
        globalCreepsAliveCount.UpdateValue(-1);
        creepsAliveCount -= 1;
    }

    public GameObject GetRandomPrefab()
    {
        var result = creepPrefabs[Random.Range(0, creepPrefabs.Size())];
        return result;
    }

}