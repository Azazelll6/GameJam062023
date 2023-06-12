using UnityEngine;

public class PerlinNoiseMap2DGen : MonoBehaviour
{
    private float _valueX, _valueY;
    private float[,] _noiseMap;
    private int _x, _y;
    
    public float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        _noiseMap = new float[mapWidth, mapHeight];

        for (_y = 0; _y < mapHeight; _y++)
        {
            for (_x = 0; _x < mapWidth; _x++)
            {
                _valueX = _x / scale;
                _valueY = _y / scale;

                _noiseMap[_x, _y] = Mathf.PerlinNoise(_valueX, _valueY);
            }
        }

        return _noiseMap;
    }
}
