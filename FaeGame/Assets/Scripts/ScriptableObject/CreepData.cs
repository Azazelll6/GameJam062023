using UnityEngine;

[CreateAssetMenu (fileName = "CreepData", menuName = "Data/CreepData/CreepData/Creep Data")]
public class CreepData : ScriptableObject
{
    public IMove movementBehavior { get; set; }
    public Transform destination { get; set; }
    public float height;
}