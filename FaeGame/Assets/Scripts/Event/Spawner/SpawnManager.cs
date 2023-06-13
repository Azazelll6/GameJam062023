using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab;
    public Transform pathingToLocation;
    public int poolSize = 20;
    private List<GameObject> _pooledObjects;

    private void Start()
    {
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefab);
            obj.SetActive(false);
            obj.GetComponent<NavAgentBehavior>().destination = pathingToLocation;
            _pooledObjects.Add(obj);
        }
        InvokeRepeating("Spawn", 1, 0.3f);
    }

    private void Spawn()
    {
        for (var i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                _pooledObjects[i].transform.position = transform.position;
                _pooledObjects[i].transform.rotation = Quaternion.identity;
                _pooledObjects[i].SetActive(true);
                break;
            }
        }
    }
}