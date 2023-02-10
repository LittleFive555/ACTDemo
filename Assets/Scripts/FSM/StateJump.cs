using System.Collections;
using UnityEngine;

public class StateJump : BaseState, IStateHandler
{
    public ActionState ActionState => ActionState.Jump;

    public bool IsDone { get; private set; }

    public StateJump(PlayerController playerController) : base(playerController)
    {
    }

    public IEnumerator Excute()
    {
        _playerController.JumpUp();
        yield return new WaitUntil(() => _playerController.IsGrounded());
        IsDone = true;
    }

    public void ShutDown()
    {

    }
}
