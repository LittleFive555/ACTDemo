using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<ActionState, IStateHandler> _states = new Dictionary<ActionState, IStateHandler>();
    private IStateHandler _currentState;

    public void AddState(ActionState stateEnum, IStateHandler stateGroup)
    {
        _states.Add(stateEnum, stateGroup);
    }

    public void RemoveState(ActionState stateEnum)
    {
        _states.Remove(stateEnum);
    }

    public void SetState(ActionState stateEnum)
    {
        if (_currentState != null)
            _currentState.Exit();
        _currentState = _states[stateEnum];
        _currentState.Enter();
    }

    public void Update()
    {
        if (_currentState != null)
            _currentState.Update();
    }
}
