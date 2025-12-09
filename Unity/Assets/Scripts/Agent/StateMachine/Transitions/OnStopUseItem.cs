using UnityEngine;

[System.Serializable]
public class OnStopUseItem : StateTransition
{
    private AgentInputProvider inputProvider;
    public OnStopUseItem(State target) : base(target) { }

    public override void Initialize(AgentContext agentContext)
    {
        inputProvider = agentContext.Get<AgentInputProvider>();
    }
    public override bool IsTriggered(AgentContext agent)
    {
        return inputProvider.InputData.UseItem == InputState.Released;
    }
}
