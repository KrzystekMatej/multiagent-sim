
using UnityEngine;

[System.Serializable]
public class OnRoll : StateTransition
{
    public OnRoll(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        return agent.Get<InputController>().InputData.Roll == InputState.Pressed;
    }
}
