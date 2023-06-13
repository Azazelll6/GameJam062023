using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentBehavior : MonoBehaviour
{
    private WaitForFixedUpdate _wffuObj = new WaitForFixedUpdate();
    private NavMeshAgent _ai;
    public Transform destination;

    private void OnEnable()
    {
        _ai = GetComponent<NavMeshAgent>();
        _ai.SetDestination(destination.position);
        StartEndPathCheck();
    }

    public void StartEndPathCheck()
    {
        StartCoroutine(EndCheck());
    }

    private IEnumerator EndCheck()
    {
        while(true)
        {
            if (_ai.remainingDistance < 0.5f && _ai.hasPath)
            {
                gameObject.SetActive(false);
                yield break;
            }
            yield return _wffuObj;
        }
    }
}