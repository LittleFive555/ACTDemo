using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private FSMExecutor _fsmExecutor;

    public ActionState CurrentState => _fsmExecutor.CurrentState;

    private void Update()
    {
        ProcessFrame();
    }

    private void ProcessFrame()
    {
        if (InputHandler.GetHoldingInput(OperationBinder.Instance.Attack))
        {
            if (_fsmExecutor.CanEnterState(ActionState.Attack, CurrentState))
            {
                _fsmExecutor.DoState( ActionState.Attack);
                return;
            }
        }

        if (InputHandler.GetHoldingInput(OperationBinder.Instance.Jump) && _playerController.IsGrounded())
        {
            if (_fsmExecutor.CanEnterState(ActionState.Jump, CurrentState))
            {
                _fsmExecutor.DoState( ActionState.Jump);
                return;
            }
        }

        if (_playerController.IsGrounded() && (InputHandler.GetHoldingInput(OperationBinder.Instance.LeftMove) || InputHandler.GetHoldingInput(OperationBinder.Instance.RightMove)))
        {
            if (_fsmExecutor.CanEnterState(ActionState.Move, CurrentState))
            {
                _fsmExecutor.DoState( ActionState.Move);
                return;
            }
        }

        if (_playerController.IsGrounded())
            Idle();
    }

    public void Idle()
    {
        if (!_fsmExecutor.CanEnterState(ActionState.Idle, CurrentState))
            return;

        _fsmExecutor.DoState( ActionState.Idle);
    }
}
