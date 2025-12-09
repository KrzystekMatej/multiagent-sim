using UnityEngine;

[System.Serializable]
public class OnStopRun : StateTransition
{
    public OnStopRun(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        InputState run = agent.Get<AgentInputProvider>().InputData.Run;
        return run == InputState.Inactive || run == InputState.Released;
    }
}
