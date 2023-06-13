using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public UnityEvent creepSpawned;

    public SpawnerData spawnerData;
    public Transform pathingToLocation;
    
    public int activeCount, maxCount = 10, poolSize = 20;
    public float spawnRate = 0.3f, startSpawnDelay = 1.0f;
    
    private GameObject prefab => spawnerData.GetRandomPrefab();
    private List<GameObject> _pooledObjects;

    private void Start()
    {
        spawnerData.ResetSpawner();
        UpdatePool();
        StartSpawn();
    }

    public void StartSpawn()
    {
        InvokeRepeating(nameof(Spawner), startSpawnDelay, spawnRate);
    }

    private void UpdatePool()
    {
        _pooledObjects = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefab);
            _pooledObjects.Add(obj);
            obj.GetComponent<NavAgentBehavior>().destination = pathingToLocation;
            obj.SetActive(false);
        }
    }

    private void Spawner()
    {
        activeCount =  spawnerData.GetAliveCount();
        if (activeCount < maxCount)
        {
            for (var i = 0; i < _pooledObjects.Count; i++)
            {
                if (!_pooledObjects[i].activeInHierarchy)
                {
                    _pooledObjects[i].transform.position = transform.position;
                    _pooledObjects[i].transform.rotation = Quaternion.identity;
                    _pooledObjects[i].SetActive(true);
                    _pooledObjects[i].GetComponent<NavAgentBehavior>().Setup(pathingToLocation);
                    spawnerData.IncrementCreepsAliveCount();
                    creepSpawned.Invoke();
                    break;
                }
            }
        }
    }

}