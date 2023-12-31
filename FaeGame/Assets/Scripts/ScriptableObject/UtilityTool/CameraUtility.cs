using UnityEngine;

[CreateAssetMenu(fileName = "CameraUtility", menuName = "UtilitySO/CameraUtil")]
public class CameraUtility : ScriptableObject, IDrawGizmo
{
    private Ray _ray;
    
    public Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    public RaycastHit PointToRay(Camera camera, Vector3 position)
    {
        RaycastHit hit;
        _ray = camera.ScreenPointToRay(position);
        if (!Physics.Raycast(_ray, out hit)) return default;
        OnDrawGizmos();
        return hit;
    }

    public void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Debug.DrawRay(_ray.origin, _ray.direction * 100, Color.red, 10);
#endif
    }
}