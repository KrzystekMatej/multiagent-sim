using UnityEngine;

[System.Serializable]
public class OnAnimationComplete : StateTransition
{
    public OnAnimationComplete(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        return true;
    }
}
