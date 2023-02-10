using System.Collections;

public class StateAttack : BaseState, IStateHandler
{
    public ActionState ActionState => ActionState.Attack;

    public bool IsDone { get; private set; }

    public StateAttack(PlayerController playerController) : base(playerController)
    {
    }

    public IEnumerator Excute()
    {
        yield return _playerController.AttackAnim();
        IsDone = true;
    }

    public void ShutDown()
    {
        throw new System.NotImplementedException();
    }
}
