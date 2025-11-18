using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : InputController
{
    private GameInput input;
    public UnityEvent OnMenuKeyPressed;

    private void Awake()
    {
        input = new GameInput();
    }

    private void OnEnable() => input.Gameplay.Enable();
    private void OnDisable() => input.Gameplay.Disable();

    private void Update()
    {
        inputData.MoveInput = input.Gameplay.Move.ReadValue<Vector2>();
        inputData.Jump = GetInputState(input.Gameplay.Jump);
        inputData.Attack = GetInputState(input.Gameplay.Attack);
        inputData.Run = GetInputState(input.Gameplay.Run);
        inputData.Roll = GetInputState(input.Gameplay.Roll);


        if (input.Gameplay.Menu.WasPressedThisFrame())
            OnMenuKeyPressed?.Invoke();
    }

    private InputState GetInputState(InputAction action)
    {
        if (action.WasPressedThisFrame()) return InputState.Pressed;
        if (action.WasReleasedThisFrame()) return InputState.Released;
        return action.IsPressed() ? InputState.Held : InputState.Inactive;
    }
}
