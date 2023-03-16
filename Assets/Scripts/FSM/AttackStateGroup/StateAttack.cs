using System.Collections;

public class StateAttack : BaseState, IStateHandler
{
    public StateAttack(PlayerController playerController) : base(playerController)
    {
    }

    public void Enter()
    {
        _playerController.AttackAnim();
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
