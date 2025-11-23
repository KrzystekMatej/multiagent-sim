using UnityEngine;

[System.Serializable]
public class OnRun : StateTransition
{
    public OnRun(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        InputState run = agent.Get<AgentInputProvider>().InputData.Run;
        return run == InputState.Pressed || run == InputState.Held;
    }
}
