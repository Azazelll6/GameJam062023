using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentBehavior : MonoBehaviour
{
    public UnityEvent creepReachedBase;
    
    private WaitForFixedUpdate _wffuObj;
    private NavMeshAgent _ai;
    public Transform destination;

    private void Awake()
    {
        _wffuObj = new WaitForFixedUpdate();
        _ai = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _ai.SetDestination(destination.position);
        StartEndPathCheck();
    }

    public void Setup(Transform dest)
    {
        this.destination = dest;
        _ai.SetDestination(destination.position);
    }


    private void StartEndPathCheck()
    {
        StartCoroutine(EndCheck());
    }

    private IEnumerator EndCheck()
    {
        while (true)
        {
            if (_ai.remainingDistance < 0.5f && _ai.hasPath)
            {
                gameObject.SetActive(false);
                creepReachedBase.Invoke();
                yield break;
            }

            yield return _wffuObj;
        }
    }
}