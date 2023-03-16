using System.Collections;
using UnityEngine;

public class StateJump : BaseState, IStateHandler
{
    public StateJump(PlayerController playerController) : base(playerController)
    {
    }

    public void Enter()
    {
        _playerController.JumpUp();
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }
}
