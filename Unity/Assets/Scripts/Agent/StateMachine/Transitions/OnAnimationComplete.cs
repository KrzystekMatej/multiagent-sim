using UnityEngine;

[System.Serializable]
public class OnAnimationComplete : EventTriggeredTransition
{
    public OnAnimationComplete(State target) : base(target) { }

    public override void Initialize(AgentContext agent)
    {
        agent.Get<AnimatorManager>().OnAnimationComplete.AddListener(Trigger);
    }
}
