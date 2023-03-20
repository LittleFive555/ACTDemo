using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance;

    public delegate void OnUpdate();
    public event OnUpdate UpdateCallback;

    public delegate void OnLateUpdate();
    public event OnLateUpdate LateUpdateCallback;

    public delegate void OnFixedUpdate();
    public event OnFixedUpdate FixedUpdateCallback;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateCallback?.Invoke();
    }

    private void LateUpdate()
    {
        LateUpdateCallback?.Invoke();
    }

    private void FixedUpdate()
    {
        FixedUpdateCallback?.Invoke();
    }
}