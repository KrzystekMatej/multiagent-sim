using UnityEngine;

[System.Serializable]
public class OnRunEnd : StateTransition
{
    public OnRunEnd(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        InputState run = agent.Get<InputController>().InputData.Run;
        return run == InputState.Inactive || run == InputState.Released;
    }
}
