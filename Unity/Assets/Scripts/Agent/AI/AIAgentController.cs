using UnityEngine;

public class AIAgentController : AgentComponent, IAgentInputSource
{
    private AgentInputData inputData = new AgentInputData();

    private Vector2 suggestedSteeringForce;
    private bool stopRequest;
    private bool forceRequest;
    private bool jumpRequest;
    private bool attackRequest;

    public AgentInputData ProvideInputSource() => inputData;

    private void Update()
    {
        inputData.Jump = GetFixedInput(inputData.Jump, jumpRequest);
        inputData.UseItem = GetFixedInput(inputData.UseItem, attackRequest);
        suggestedSteeringForce = Vector2.zero;
        forceRequest = false;
        attackRequest = false;
    }

    private InputState GetFixedInput(InputState state, bool actionRequest)
    {
        const int inactiveDown = ((int)InputState.Inactive * 2) ^ 1;
        const int pressedUp = ((int)InputState.Pressed * 2) ^ 0;
        const int pressedDown = ((int)InputState.Pressed * 2) ^ 1;
        const int heldUp = ((int)InputState.Held * 2) ^ 0;
        const int releasedUp = ((int)InputState.Released * 2) ^ 0;
        const int releasedDown = ((int)InputState.Released * 2) ^ 1;

        int key = ((int)state * 2) ^ (actionRequest ? 1 : 0);

        switch (key)
        {
            case inactiveDown:
                return InputState.Pressed;
            case pressedUp:
                return InputState.Released;
            case pressedDown:
                return InputState.Held;
            case heldUp:
                return InputState.Released;
            case releasedUp:
                return InputState.Inactive;
            case releasedDown:
                return InputState.Pressed;
            default:
                return state;
        }
    }

    public void StopMoving()
    {
        stopRequest = true;
    }

    public void AddSteeringForce(Vector2 steeringForce)
    {
        if (steeringForce == Vector2.zero) return;
        suggestedSteeringForce += steeringForce;
        forceRequest = true;
    }

    public void StartJumping()
    {
        jumpRequest = true;
    }

    public void StopJumping()
    {
        jumpRequest = false;
    }

    public void Attack()
    {
        attackRequest = true;
    }
}
