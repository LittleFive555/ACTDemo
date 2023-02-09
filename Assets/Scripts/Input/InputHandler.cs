using UnityEngine;

public class InputHandler
{
    public static bool GetInput(Operation operation)
    {
        return Input.GetKey(operation.TriggerKey);
    }
}