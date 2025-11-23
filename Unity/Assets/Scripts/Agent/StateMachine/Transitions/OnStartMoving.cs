using UnityEngine;

[System.Serializable]
public class OnStartMoving : StateTransition
{
    public OnStartMoving(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        return agent.Get<AgentInputProvider>().InputData.MoveInput.magnitude >= Mathf.Epsilon;
    }
}
