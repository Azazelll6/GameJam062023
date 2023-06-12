using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavAgentBehavior : MonoBehaviour, IDamagable, IDamageDealer
{
    private IMove _movementBehavior;
    private WaitForFixedUpdate _wffuObj = new WaitForFixedUpdate();
    private Coroutine _moveCoroutine;

    public Transform destination;

    private void Awake()
    {
        _movementBehavior = GetComponent<IMove>();
        Debug.Log(_movementBehavior);
        SetMoveBehavior(_movementBehavior);
        if (_movementBehavior is INeedsTransform needsTransform)
        {
            Debug.Log("Need Transform");
            needsTransform.SetTransform(destination);
        }
        
        StartMovement();
    }

    public void SetMoveBehavior(IMove behavior)
    {
        _movementBehavior = behavior;
    }

    private void StartMovement()
    {
        _moveCoroutine = StartCoroutine(Move());
    }

    public void StopMovement()
    {
        if (_moveCoroutine != null) StopCoroutine(_moveCoroutine);
    }

    private IEnumerator Move()
    {
        while(true)
        {
            _movementBehavior?.Move();
            yield return _wffuObj;
        }
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
