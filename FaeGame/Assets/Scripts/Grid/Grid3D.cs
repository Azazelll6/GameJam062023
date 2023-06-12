using UnityEngine;

public class Grid3D<T>
{
    private int width;
    private int height;
    private int depth;
    private T[,,] gridArray;

    public Grid3D(int width, int height, int depth)
    {
        this.width = width;
        this.height = height;
        this.depth = depth;

        gridArray = new T[width, height, depth];
    }

    public void SetValue(int x, int y, int z, T value)
    {
        if (x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth)
        {
            gridArray[x, y, z] = value;
        }
    }

    public T GetValue(int x, int y, int z)
    {
        if (x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth)
        {
            return gridArray[x, y, z];
        }
        else
        {
            return default(T);
        }
    }
}

