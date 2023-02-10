using System.Collections;
using UnityEngine;

public class FSMExecutor : MonoBehaviour
{
    [SerializeField]
    private FSMStateTransitions _fsmStateTransitions;

    public IStateHandler CurrentStateHandler { get; private set; }
    private Coroutine _currentStateCoroutine = null;

    public bool CanEnterState(ActionState targetState, ActionState currentState)
    {
        return _fsmStateTransitions.CouldEnter(targetState, currentState);
    }

    public void DoState(IStateHandler state)
    {
        if (_currentStateCoroutine != null)
            StopCoroutine(_currentStateCoroutine);
        _currentStateCoroutine = StartCoroutine(DoStateCoroutine(state));
    }

    public void ShutDown()
    {
        CurrentStateHandler?.ShutDown();
    }

    private IEnumerator DoStateCoroutine(IStateHandler state)
    {
        CurrentStateHandler = state;
        yield return state.Excute();
    }
}