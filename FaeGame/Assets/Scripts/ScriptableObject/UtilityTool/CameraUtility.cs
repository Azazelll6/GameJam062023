using UnityEngine;

[CreateAssetMenu(fileName = "CameraUtility", menuName = "UtilitySO/CameraUtil")]
public class CameraUtility : ScriptableObject
{
    public Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }

    public Vector3 ScreenPointToRay(Camera camera, Vector3 position)
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(position);
        Debug.Log("RAY?" + ray);
        if (!Physics.Raycast(ray, out hit)) return Vector3.zero;
        Debug.Log("POINT?" + hit.point);
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red, 10);
        return hit.point;
    }
}