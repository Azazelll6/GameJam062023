using UnityEngine;

[CreateAssetMenu (fileName = "CharacterData", menuName = "Data/ControllerData/CharacterData")]

public class CharacterData : ScriptableObject
{
    //public ID id;
    public float speed, topSpeed, knockBackPower, knockBackResistance;
    public BoolData canRun, gameOver;
}
