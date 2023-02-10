using UnityEngine;

public class InputHandler
{
    public static bool GetHoldingInput(Operation operation)
    {
        return Input.GetKey(operation.TriggerKey);
    }

    public static bool GetInput(Operation operation)
    {
        return Input.GetKeyDown(operation.TriggerKey);
    }
}