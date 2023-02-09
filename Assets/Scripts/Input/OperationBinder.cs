using UnityEngine;

public class OperationBinder
{
    private static OperationBinder _instance;
    public static OperationBinder Instance
    { 
        get
        {
            if (_instance == null)
                _instance = new OperationBinder();
            return _instance;
        }
    }

    public Operation LeftMove { get; private set; }
    public Operation RightMove { get; private set; }
    public Operation Jump { get; private set; }
    public Operation Attack { get; private set; }

    public OperationBinder()
    {
        LeftMove = new Operation("LeftMove", KeyCode.LeftArrow);
        RightMove = new Operation("RightMove", KeyCode.RightArrow);
        Jump = new Operation("Jump", KeyCode.Space);
        Attack = new Operation("Attack", KeyCode.A);
    }
}
