using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInput _controller;
    public Vector2 Move
    {
        get; private set;
    }
    private int _killRequestedFrame = -1;
    private bool _killConsumedThisFrame;

    private void Awake()
    {
        _controller = InputRoot.Instance.Controller;
    }

    private void OnEnable()
    {
        _controller.Base.Move.performed += OnMove;
        _controller.Base.Move.canceled += OnMove;
        _controller.Base.Kill.performed += OnKill;
    }

    private void OnDisable()
    {
        if (_controller == null)
            return;
        _controller.Base.Move.performed -= OnMove;
        _controller.Base.Move.canceled -= OnMove;
        _controller.Base.Kill.performed -= OnKill;
    }

    private void LateUpdate()
    {
        //If kill button was pressed, but nobody consumed it this frame, discard it
        if (_killRequestedFrame != Time.frameCount)
        {
            _killConsumedThisFrame = true;
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnKill(InputAction.CallbackContext context)
    {
        _killRequestedFrame = Time.frameCount;
        _killConsumedThisFrame = false;
    }

    public bool ConsumeKillRequest()
    {
        if (_killRequestedFrame != Time.frameCount)
            return false;
        if (_killConsumedThisFrame)
            return false;
        _killConsumedThisFrame = true;
        return true;
    }
}
