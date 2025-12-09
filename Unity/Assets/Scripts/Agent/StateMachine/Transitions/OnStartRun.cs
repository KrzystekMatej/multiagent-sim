using UnityEngine;

[System.Serializable]
public class OnStartRun : StateTransition
{
    public OnStartRun(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        InputState run = agent.Get<AgentInputProvider>().InputData.Run;
        return run == InputState.Pressed || run == InputState.Held;
    }
}
