using UnityEngine;

[CreateAssetMenu(fileName = "PrefabData", menuName = "Data/SingleValueData/PrefabData")]
public class PrefabData : ScriptableObject
{
    public GameObject prefab;
    public int priority;
}
