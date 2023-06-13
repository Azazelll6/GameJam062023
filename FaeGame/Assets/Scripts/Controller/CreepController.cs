using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(NavAgentBehavior))]
public class CreepController : MonoBehaviour, IDamagable, IDamageDealer
{
    public CreepData creepData;
    
    private NavAgentBehavior _agentBehavior;
    private NavMeshAgent _agent;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = creepData.speed;
    }
    
    public void TakeDamage(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void DealDamage(IDamagable target, float amount)
    {
        throw new System.NotImplementedException();
    }
}
