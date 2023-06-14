using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class GridGenerator : MonoBehaviour, INeedButton, IUpdateOnChange
{
    public PrefabDataList groundPrefabData;
    public TransformArrayData grid;
    [SerializeField]
    private int width = 5;
    [SerializeField]
    private int length = 10;
    [SerializeField]
    private int height = 1;
    [Range(0f, 1f)]
    [Step(0.01f)]
    public float heightOffset = 1f;
    private GameObject _groundPrefab;
    private Vector3 _prefabScale;
    
    private GameObject _ground;

    private WaitForFixedUpdate _wffu;

    private void Awake()
    {
        _prefabScale = groundPrefabData.GetRandomPrefab().transform.localScale;
        _wffu = new WaitForFixedUpdate();
        ResetGround();
    }

    private void CreateGround()
    {
        if (width <= 0 || length <= 0)
        {
            return;
        }

        grid.InitializeArraySize(width, length);
        for (int i = 0; i < width; i++) 
        {
            _groundPrefab = groundPrefabData.GetRandomPrefab();
            _prefabScale = _groundPrefab.transform.localScale;
            for (int j = 0; j < length; j++)
            {
                float randomHeight = Random.Range(0, heightOffset);
                GameObject cell = Instantiate(_groundPrefab,
                        new Vector3(i * _prefabScale.x - (width * _prefabScale.x) / 2f + _prefabScale.x / 2f,
                            0 + randomHeight,
                            j * _prefabScale.z - (length * _prefabScale.z) / 2f + _prefabScale.z / 2f),
                        Quaternion.identity);

                cell.transform.localScale = new Vector3(_prefabScale.x, height, _prefabScale.z);

                cell.transform.SetParent(_ground.transform);

                grid[i, j] = cell.transform;
            }
            
        }
        
        var navMeshSurface = _ground.GetComponent<NavMeshSurface>();if (navMeshSurface == null)
        {
            navMeshSurface = _ground.AddComponent<NavMeshSurface>();
        }
        navMeshSurface.collectObjects = CollectObjects.Volume;
        navMeshSurface.size = new Vector3(width * _prefabScale.x, height + heightOffset + 3, length * _prefabScale.z);
        navMeshSurface.center = new Vector3(0, 1, 0);
        navMeshSurface.BuildNavMesh();
    }

    public string GetButtonName()
    {
        return "Generate New";
    }
    
    public void ButtonAction()
    {
        StartCoroutine(DelayedResetGround());
    }

    [ContextMenu("Reset Ground")]
    public void ResetGround()
    {
        _ground = GameObject.Find("Ground");
        
        if(_ground == null)
        {
            _ground = new GameObject("Ground");
        }
        else
        {
            foreach (Transform child in _ground.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        _ground.transform.position = new Vector3(0,0,0);
        CreateGround();
    }
    
    private void OnDisable()
    {
        if (_ground != null)
        {
            Destroy(_ground);
        }
    }

    private void OnDestroy()
    {
        if (_ground != null)
        {
            Destroy(_ground);
        }
    }

    public void OnValidate()
    {
        if(Application.isPlaying)
        {
            StartCoroutine(DelayedResetGround());
        }
    }

    private IEnumerator DelayedResetGround()
    {
        yield return _wffu;
        ResetGround();
    }
}