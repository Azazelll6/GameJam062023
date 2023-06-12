using UnityEngine;
using UnityEngine.AI;

public class TravelToDestination : MonoBehaviour, IMove, INeedsTransform
{
    private Transform _destination;
    private NavMeshAgent _ai;

    private void Awake()
    {
        _ai = GetComponent<NavMeshAgent>();
    }

    public void SetTransform(Transform destination)
    {
        _destination = destination;
    }

    public void Move()
    {
        Debug.Log("Moving");
        if (_ai.destination != _destination.position)
        {
            _ai.SetDestination(_destination.position);
        }
    }
}

