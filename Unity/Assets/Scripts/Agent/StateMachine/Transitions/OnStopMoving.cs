using UnityEngine;

[System.Serializable]
public class OnStopMoving : StateTransition
{
    public OnStopMoving(State target) : base(target) { }
    public override bool IsTriggered(AgentContext agent)
    {
        return agent.Get<InputController>().InputData.MoveInput.magnitude < Mathf.Epsilon;
    }
}
