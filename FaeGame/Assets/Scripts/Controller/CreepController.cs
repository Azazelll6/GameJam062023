using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavAgentBehavior))]
public class CreepController : MonoBehaviour
{
    public CreepData creepData;

    private NavMeshAgent _navAgent;
    private NavAgentBehavior _agentBehavior;

    private void Awake()
    {
        throw new NotImplementedException();
    }
}
