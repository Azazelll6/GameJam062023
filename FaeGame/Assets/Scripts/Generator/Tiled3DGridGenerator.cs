using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Generate3DNavMeshSurface))]
public class Tiled3DGridGenerator : MonoBehaviour, INeedButton
{
    public GameAction triggerNavRebuild;
    private Generate3DNavMeshSurface _navMeshGen;
    public TileDataList groundTileData;
    public TileArrayData grid;
    public Vector3Data navSize;
    private TileData _tileData;
    [Range(0f, 1f)]
    [Step(0.01f)]
    public float heightOffset = 1f;
    [SerializeField]
    private int width = 5;
    [SerializeField]
    private int length = 10;
    [SerializeField]
    private int startHeight = 0;
    
    private GameObject _groundParent, _groundPrefab, _occupiedPrefab;
    private Vector3 _prefabSize;
    private GroundBehavior _groundBehavior;
    private bool _isResetting;
    private WaitForFixedUpdate _wffu;
    
    //variables for editor updating
    //private int _prevWidth, _prevLength, _prevHeight;
    //private float _prevHeightOffset, _resetDelay;
    
    private void Awake()
    {
        _groundParent = GameObject.Find("Ground");
        _occupiedPrefab = groundTileData.GetOccupiedPrefab();
        _navMeshGen = GetComponent<Generate3DNavMeshSurface>();
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
            for (int j = 0; j < length; j++)
            {
                _tileData = ScriptableObject.CreateInstance<TileData>();
                _tileData.gridCoord = new Vector2Int(i, j);
                _groundPrefab = groundTileData.GetRandomPriorityPrefab();
                var meshRenderer = _groundPrefab.GetComponentInChildren<MeshRenderer>();
                Bounds bounds = meshRenderer.bounds;
                _prefabSize = bounds.size;
                _groundPrefab.transform.localScale = new Vector3(.32f, 1, .32f);

                    float randomHeight = Random.Range(0, heightOffset);
                GameObject cell = Instantiate(_groundPrefab,
                    new Vector3(i * _prefabSize.x - (width * _prefabSize.x) / 2f + _prefabSize.x / 2f,
                         startHeight + randomHeight,
                        j * _prefabSize.z - (length * _prefabSize.z) / 2f + _prefabSize.z / 2f),
                    Quaternion.identity);

                cell.transform.SetParent(_groundParent.transform);
                
                _groundBehavior = cell.GetComponent<GroundBehavior>();
                _tileData.transform = cell.transform;
                _tileData.environmentPrefab = _groundPrefab;
                _tileData.occupiedPrefab = _occupiedPrefab;
                _tileData.groundBehavior = _groundBehavior;

                
                grid[i, j] = _tileData;
                _groundBehavior.tileData = _tileData;
                _groundBehavior.SetGrid(grid);
            }
        }
        navSize.value = new Vector3(width * _prefabSize.x, startHeight + heightOffset + 3, length * _prefabSize.z);
        triggerNavRebuild.RaiseAction();
    }

    [ContextMenu("Reset Ground")]
    public void ResetGround()
    {
        if(_groundParent == null)
        {
            _groundParent = new GameObject("Ground");
        }
        else
        {
            foreach (Transform child in _groundParent.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
        _groundParent.transform.position = new Vector3(0,0,0);
        CreateGround();
    }
    
    private void OnDisable()
    {
        if (_groundParent != null)
        {
            Destroy(_groundParent);
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
        if(Application.isPlaying && (_prevWidth != width || _prevLength != length || _prevHeight != startHeight || _prevHeightOffset != heightOffset))
        {
            _prevWidth = width;
            _prevLength = length;
            _prevHeight = startHeight;
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