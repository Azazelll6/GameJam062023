using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour, INeedButton
{
    public UnityEvent creepSpawned;

    public SpawnerData spawnerData;
    public Transform pathingToLocation;
    
    public int activeCount, numToSpawn = 10, poolSize = 10;
    public float spawnRate = 0.3f, spawnDelay = 1.0f;

    private int spawnedCount;

    private WaitForSeconds _waitForSpawnRate, _waitForSpawnDelay;
    private List<GameObject> _pooledObjects;

    private void Start()
    {
        _waitForSpawnRate = new WaitForSeconds(spawnRate);
        _waitForSpawnDelay = new WaitForSeconds(spawnDelay);
        spawnerData.ResetSpawner();
        CreatePool();
    }

    public void StartSpawn(int amount)
    {
        numToSpawn = amount;
        StartCoroutine(Spawner());
    }

    private void CreatePool()
    {
        _pooledObjects = new List<GameObject>();
        
        int totalPriority = spawnerData.prefabDataList.GetPriority();
        
        for (int i = 0; i < poolSize; i++)
        {
            int randomNumber = Random.Range(0, totalPriority);
            int sum = 0;
            foreach (PrefabData prefabData in spawnerData.prefabDataList.prefabDataList)
            {
                sum += prefabData.priority;
                if (randomNumber < sum)
                {
                    GameObject obj = (GameObject)Instantiate(prefabData.prefab);
                    prefabData.creepData.totalSpawned += 1;
                    _pooledObjects.Add(obj);
                    obj.GetComponent<NavAgentBehavior>().destination = pathingToLocation;
                    obj.SetActive(false);
                    break;
                }
            }
        }
    }


    private IEnumerator Spawner()
    {
        spawnedCount = 0;
        while (spawnedCount < numToSpawn)
        {
            activeCount =  spawnerData.GetAliveCount();
            bool spawned = SpawnFromPool();
            if (!spawned)
            {
                UpdatePoolAndSpawn();
            }
            yield return _waitForSpawnRate;
        }
    }

    private bool SpawnFromPool()
    {
        bool spawn = false;
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
                spawn = true;
                break;
            }
        }
        return spawn;
    }

    private void UpdatePoolAndSpawn()
    {
        int totalPriority = spawnerData.prefabDataList.GetPriority();
        int randomNumber = Random.Range(0, totalPriority);
        int sum = 0;
        foreach (PrefabData prefabData in spawnerData.prefabDataList.prefabDataList)
        {
            sum += prefabData.priority;
            if (randomNumber < sum)
            {
                GameObject obj = (GameObject)Instantiate(prefabData.prefab);
                prefabData.creepData.totalSpawned += 1;
                _pooledObjects.Add(obj);
                obj.transform.position = transform.position;
                obj.transform.rotation = Quaternion.identity;
                obj.GetComponent<NavAgentBehavior>().destination = pathingToLocation;
                obj.SetActive(true);
                obj.GetComponent<NavAgentBehavior>().Setup(pathingToLocation);
                spawnerData.IncrementCreepsAliveCount();
                creepSpawned.Invoke();
                break;
            }
        }
    }
    
    public void ButtonAction()
    {
       StartSpawn(numToSpawn);
    }

    public string GetButtonName()
    {
        return "Spawn";
    }
}