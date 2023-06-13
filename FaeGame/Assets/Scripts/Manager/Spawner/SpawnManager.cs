using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab;
    public Transform pathingToLocation;
    public int maxCount = 10, poolSize = 20;
    private int _activeCount = 0;
    public float spawnRate = 0.3f;
    private List<GameObject> _pooledObjects;

    private void Start()
    {
        UpdatePool();
        InvokeRepeating(nameof(Spawner), 1, spawnRate);
    }

    private void UpdatePool()
    {
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefab);
            obj.SetActive(false);
            obj.GetComponent<NavAgentBehavior>().destination = pathingToLocation;
            _pooledObjects.Add(obj);
            _pooledObjects[i].GetComponent<NavAgentBehavior>().Setup(pathingToLocation);
        }
    }


    public void DecrementActiveCount()
    {
        if (_activeCount > 0)
            _activeCount--;
    }


    private void Spawner()
    {
        if (_activeCount < maxCount)
        {
            for (var i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    _pooledObjects[i].transform.position = transform.position;
                    _pooledObjects[i].transform.rotation = Quaternion.identity;
                    _pooledObjects[i].SetActive(true);
                    _activeCount++;
                    break;
                }
            }
        }
    }
}