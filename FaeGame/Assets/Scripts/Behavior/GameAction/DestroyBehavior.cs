using UnityEngine;

public class DestroyBehavior : MonoBehaviour
{
    public void DestroySelfAndChild()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
        
        Destroy(gameObject);
    }
}
