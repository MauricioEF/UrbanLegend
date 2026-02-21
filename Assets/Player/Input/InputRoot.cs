using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class InputRoot : MonoBehaviour
{
    public static InputRoot Instance
    {
        get; private set;
    }
    public PlayerInput Controller;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
        Controller = new PlayerInput();
        Controller.Enable();
    }
}
