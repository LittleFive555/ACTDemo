using System.Collections;
using UnityEngine;

public class StateMove : BaseState, IStateHandler
{
    public StateMove(PlayerController playerController) : base(playerController)
    {
    }

    public void Enter()
    {

    }

    public void Update()
    {
        float velocity = 0;
        if (InputHandler.GetHoldingInput(OperationBinder.Instance.LeftMove))
            velocity -= _playerController.Character.MoveSpeed;
        if (InputHandler.GetHoldingInput(OperationBinder.Instance.RightMove))
            velocity += _playerController.Character.MoveSpeed;

        // Turn
        if (velocity - 0 > 0.000001f)
            _playerController.SetModelFacing(true);
        else if (velocity - 0 < -0.000001f)
            _playerController.SetModelFacing(false);
        else
            return;

        float step = Time.deltaTime * velocity;
        _playerController.transform.position += Vector3.right * step;
    }

    public void Exit()
    {

    }
}