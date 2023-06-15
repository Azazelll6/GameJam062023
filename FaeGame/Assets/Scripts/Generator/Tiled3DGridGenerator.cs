using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Generate3DNavMeshSurface))]
public class Tiled3DGridGenerator : MonoBehaviour, INeedButton
{
    private Generate3DNavMeshSurface _navMeshGen;
    public PrefabDataList groundPrefabData;
    public TransformArrayData grid;
    [Range(0f, 1f)]
    [Step(0.01f)]
    public float heightOffset = 1f;
    [SerializeField]
    private int width = 5;
    [SerializeField]
    private int length = 10;
    [SerializeField]
    private int height = 1;
    
    private GameObject _ground, _groundPrefab;
    private Vector3 _prefabScale;
    private int _prevWidth, _prevLength, _prevHeight;
    private float _prevHeightOffset;//, _resetDelay;
    private bool _isResetting;
    private WaitForFixedUpdate _wffu;

    private void Awake()
    {
        _navMeshGen = GetComponent<Generate3DNavMeshSurface>();
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
        _navMeshGen.BuildNavMeshSurfaceToParent(width * _prefabScale.x, height + heightOffset + 3, length * _prefabScale.z, _ground);
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

    public string GetButtonName()
    {
        return "Generate New";
    }
    
    public void ButtonAction()
    {
        if (_isResetting) return;
        StartCoroutine(DelayedResetGround());
    }
    
/* Use this only to find the height offset you want. It duplicates the grid objects and breaks the navmesh. 
    public void OnValidate()
    {
        _resetDelay = 0.5f;
        if(Application.isPlaying && (_prevWidth != width || _prevLength != length || _prevHeight != height || _prevHeightOffset != heightOffset))
        {
            _prevWidth = width;
            _prevLength = length;
            _prevHeight = height;
            _prevHeightOffset = heightOffset;
            StartCoroutine(DelayedResetGround());
        }
    }
*/
    private IEnumerator DelayedResetGround()
    {
        _isResetting = true;
        yield return _wffu;
        ResetGround();
        _isResetting = false;
    }
}