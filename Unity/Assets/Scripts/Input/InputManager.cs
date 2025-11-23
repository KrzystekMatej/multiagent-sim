using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }
    private PlayerInput input;

    private PlayerAgentController playerAgentController;
    private RTSController rtsController;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        playerAgentController = GetComponent<PlayerAgentController>();
        rtsController = GetComponent<RTSController>();

        InputActions = new PlayerInputActions();
    }

    public void OnTakeAgentControl(AgentContext agent)
    {
        playerAgentController.BindAgent(agent);
        playerAgentController.enabled = true;
    }

    public void OnReleaseAgentControl()
    {
        playerAgentController.enabled = false;
    }
}
