using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "Data/SingleValueData/PrefabData")]
public class PrefabData : ScriptableObject
{
    public GameObject prefab;
    public CreepData creepData;
    public int priority;
}
