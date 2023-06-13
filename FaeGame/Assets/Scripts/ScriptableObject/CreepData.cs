using UnityEngine;

[CreateAssetMenu (fileName = "CreepData", menuName = "Data/ControllerData/CreepData")]
public class CreepData : ScriptableObject
{
    public string unitName, type;
    public float speed, height, radius;
}