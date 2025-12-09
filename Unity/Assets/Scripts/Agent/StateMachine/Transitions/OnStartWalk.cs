using UnityEngine;

[System.Serializable]
public class OnStartWalk : StateTransition
{
    public OnStartWalk(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        return agent.Get<AgentInputProvider>().InputData.MoveInput.magnitude >= Mathf.Epsilon;
    }
}
