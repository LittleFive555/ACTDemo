using System.Collections;
using UnityEngine;

public class StateMove : BaseState, IStateHandler
{
    public ActionState ActionState => ActionState.Move;

    public bool IsDone { get; private set; }

    public StateMove(PlayerController playerController) : base(playerController)
    {
    }

    public IEnumerator Excute()
    {
        while (true)
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
                break;

            float step = Time.deltaTime * velocity;
            _playerController.transform.position += Vector3.right * step;
            yield return null;
        }

        IsDone = true;
    }

    public void ShutDown()
    {

    }
}