using UnityEngine;

public class Operation
{
    public string Name { get; private set; }

    public KeyCode TriggerKey { get; private set; }

    public Operation(string name, KeyCode triggerKey)
    {
        Name = name;
        TriggerKey = triggerKey;
    }
}
