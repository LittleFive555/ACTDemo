using System.Collections;
using UnityEngine;

public class StateIdle : BaseState, IStateHandler
{
    public ActionState ActionState => ActionState.Idle;

    public bool IsDone => false;

    public StateIdle(PlayerController playerController) : base(playerController)
    {
    }

    public IEnumerator Excute()
    {
        while (true)
            yield return null;
    }

    public void ShutDown()
    {

    }
}
