using UnityEngine;

public abstract class EventTriggeredTransition : StateTransition
{
    protected bool triggered;

    public EventTriggeredTransition(State target) : base(target) { }

    public virtual void Trigger()
    {
        triggered = true;
    }

    public override bool IsTriggered(AgentContext agent)
    {
        bool result = triggered;
        if (triggered) triggered = false;
        return result;
    }
}
