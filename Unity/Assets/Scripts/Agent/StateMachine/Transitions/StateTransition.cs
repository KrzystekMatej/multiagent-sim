using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class StateTransition
{
    public State Target { get; protected set; }

    public StateTransition(State target)
    {
        Target = target;
    }

    public virtual void Initialize(AgentContext agentContext) { }
    public virtual void PerformTransitionAction(AgentContext agent) { }
    public abstract bool IsTriggered(AgentContext agent);
}

