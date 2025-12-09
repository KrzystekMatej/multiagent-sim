using UnityEngine;

[System.Serializable]
public class OnZeroHealth : StateTransition
{
    private HealthManager healthManager;
    public OnZeroHealth(State target) : base(target) { }

    public override void Initialize(AgentContext agentContext)
    {
        healthManager = agentContext.Get<HealthManager>();
    }
    public override bool IsTriggered(AgentContext agent)
    {
        return healthManager.Health <= 0;
    }
}
