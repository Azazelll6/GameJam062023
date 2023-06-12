using UnityEngine;
using UnityEngine.Events;

public class GameActionHandler : MonoBehaviour
{
    public GameAction gameActionObj;
    public UnityEvent onRaiseEvent, startEvent;

    private void Start()
    {
        startEvent.Invoke();
    }

    private void OnEnable()
    {
        gameActionObj.raise += Raise;
    }

    private void OnDisable()
    {
        gameActionObj.raise -= Raise;
    }

    private void Raise()
    {
        onRaiseEvent.Invoke();
    }
}