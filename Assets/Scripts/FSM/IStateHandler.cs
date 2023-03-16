using System.Collections;

public enum ActionState
{
    None = 0,
    Idle = 1,
    Move = 2,
    Jump = 3,
    Attack = 4,
}

public interface IStateHandler
{
    public void Enter();
    public void Exit();
    public void Update();
}