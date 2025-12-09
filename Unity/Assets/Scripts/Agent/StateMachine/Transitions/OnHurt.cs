using UnityEngine;

[System.Serializable]
public class OnHurt : EventTriggeredTransition
{
    public OnHurt(State target) : base(target) { }

    public override void Initialize(AgentContext agent)
    {
        agent.Get<HealthManager>().OnHealthChanged.AddListener(difference =>
        {
            if (difference < 0) Trigger();
        });
    }
}
