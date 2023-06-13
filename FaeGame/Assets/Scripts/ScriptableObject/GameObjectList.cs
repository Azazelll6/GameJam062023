using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "GameObjectList", menuName = "Data/List/GameObjectList")]
public class GameObjectList : ScriptableObject
{
    public List<GameObject> gameObjectList;
}
