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
    public ActionState ActionState { get; }
    public bool IsDone { get; }
    public IEnumerator Excute();
    public void ShutDown();
}