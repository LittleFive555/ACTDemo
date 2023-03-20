using UnityEngine;

public class FSMExecutor : MonoBehaviour
{
    [SerializeField]
    private FSMStateTransitions _fsmStateTransitions;

    [SerializeField]
    private PlayerController _playerController;

    private StateMachine _stateMachine = new StateMachine();

    public ActionState CurrentState { get; private set; }

    private void Awake()
    {
        _stateMachine.AddState(ActionState.Idle, new StateIdle(_playerController));
        _stateMachine.AddState(ActionState.Move, new StateMove(_playerController));
        _stateMachine.AddState(ActionState.Jump, new StateJump(_playerController));
        _stateMachine.AddState(ActionState.Attack, new StateAttack(_playerController));
    }

    private void Start()
    {
        DoState(ActionState.Idle);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public bool CanEnterState(ActionState targetState, ActionState currentState)
    {
        return _fsmStateTransitions.CouldEnter(targetState, currentState);
    }

    public void DoState(ActionState stateEnum)
    {
        CurrentState = stateEnum;
        _stateMachine.SetState(stateEnum);
    }
}