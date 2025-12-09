using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAgentController : MonoBehaviour, IAgentInputSource
{
    [SerializeField]
    private Transform eye;
    private AgentInputProvider inputProvider;

    private PlayerInputActions inputActions;
    private AgentInputData inputData = new AgentInputData();

    public AgentInputData ProvideInputSource() => inputData;

    private void Start()
    {
        inputActions = GetComponent<InputManager>().InputActions;
        inputActions.AgentControl.Enable();
        inputActions.AgentControl.InventorySlot.performed += OnInventorySlotChanged;
    }

    private void OnEnable()
    {
        inputActions?.AgentControl.Enable();
        inputProvider.SetSource(this);
    }

    private void OnDisable()
    {
        inputActions.AgentControl.Disable();
        inputProvider.ResetSource();

        if (eye == null) return;
        if (eye.gameObject == null) return;
        if (eye.transform == null) return;

        eye.transform.SetParent(null, true);
    }

    private void Update()
    {
        inputData.MoveInput = inputActions.AgentControl.Move.ReadValue<Vector2>();
        inputData.Jump = GetInputState(inputActions.AgentControl.Jump);
        inputData.UseItem = GetInputState(inputActions.AgentControl.UseItem);
        inputData.Run = GetInputState(inputActions.AgentControl.Run);
        inputData.Roll = GetInputState(inputActions.AgentControl.Roll);

        bool isActive = inputData.MoveInput.sqrMagnitude > 0.0f ||
            inputData.Jump == InputState.Pressed ||
            inputData.UseItem == InputState.Pressed ||
            inputData.Run == InputState.Pressed ||
            inputData.Roll == InputState.Pressed;

        if (isActive) eye.localPosition = Vector3.zero;
    }

    private InputState GetInputState(InputAction action)
    {
        if (action.WasPressedThisFrame()) return InputState.Pressed;
        if (action.WasReleasedThisFrame()) return InputState.Released;
        return action.IsPressed() ? InputState.Held : InputState.Inactive;
    }

    public void BindAgent(AgentContext agent)
    {
        inputProvider = agent.Get<AgentInputProvider>();
        eye.transform.SetParent(agent.transform, false);
        eye.transform.localPosition = Vector3.zero;
    }

    private void OnInventorySlotChanged(InputAction.CallbackContext context)
    {
        inputProvider.SelectInventorySlot(Mathf.RoundToInt(context.ReadValue<float>()));
    }
}
