using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateTransition
{
    [SerializeField]
    public ActionState State;

    [SerializeField]
    public List<ActionState> CanEnterFromStates;
}

[CreateAssetMenu(fileName = "FSMStateTransitions", menuName = "FSM State Transitions")]
public class FSMStateTransitions : ScriptableObject
{
    [SerializeField]
    private List<StateTransition> _transtions;

    private Dictionary<ActionState, List<ActionState>> _transtionsDic;

    public bool CouldEnter(ActionState targetState, ActionState currentState)
    {
        if (_transtions == null || _transtions.Count <= 0)
            return false;

        BuildDicIfNecessary();

        return _transtionsDic.TryGetValue(targetState, out List<ActionState> canEnterFromStates) && canEnterFromStates.Contains(currentState);
    }

    private void BuildDicIfNecessary()
    {
        if (_transtionsDic == null)
        {
            _transtionsDic = new Dictionary<ActionState, List<ActionState>>();
            foreach (var transtion in _transtions)
                _transtionsDic.Add(transtion.State, transtion.CanEnterFromStates);
        }
    }
}
