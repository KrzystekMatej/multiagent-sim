using UnityEngine;

[System.Serializable]
public class OnAttack : StateTransition
{
    public OnAttack(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        return agent.Get<AgentInputProvider>().InputData.Attack == InputState.Pressed;
    }
}
