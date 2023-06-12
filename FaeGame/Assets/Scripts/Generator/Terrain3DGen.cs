using UnityEngine;

[RequireComponent(typeof(PerlinNoiseMap2DGen), typeof(MeshFilter), typeof(MeshRenderer))]
public class Terrain3DGen : MonoBehaviour
{
    private PerlinNoiseMap2DGen _generateNoiseMap;
    
    public float heightMultiplier = 1.5f;
    public Mesh mesh;
    
    public MeshFilter meshFilter;

    private int[] _triangles;
    private Vector3[] _vertices;
    private float[,] _noiseMap;
    private int _vertIndex, _triIndex;

    [SerializeField] private int width=10, height=10;
    [SerializeField] private float scale=1.5f;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        _generateNoiseMap = GetComponent<PerlinNoiseMap2DGen>();
        
        if (_generateNoiseMap == null) {
            Debug.LogError("PerlinNoiseMap2DGen component not found on this GameObject.");
            return;
        }
        
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {

        _noiseMap = _generateNoiseMap.GenerateNoiseMap(width, height, scale);

        _vertices = new Vector3[width * height];
        _triangles = new int[(width - 1) * (height - 1) * 6];

        _vertIndex = 0;
        _triIndex = 0;

        for (var j = 0; j < height; j++)
        {
            for (var i = 0; i < width; i++)
            {
                _vertices[_vertIndex] = new Vector3(i, _noiseMap[i, j] * heightMultiplier, j);

                if (i < width - 1 && j < height - 1)
                {
                    _triangles[_triIndex] = _vertIndex;
                    _triangles[_triIndex + 1] = _vertIndex + width;
                    _triangles[_triIndex + 2] = _vertIndex + width + 1;

                    _triangles[_triIndex + 3] = _vertIndex;
                    _triangles[_triIndex + 4] = _vertIndex + width + 1;
                    _triangles[_triIndex + 5] = _vertIndex + 1;
                    _triIndex += 6;
                }

                _vertIndex++;
            }
        }

        mesh.vertices = _vertices;
        mesh.triangles = _triangles;
        mesh.RecalculateNormals();

        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
    }
}
