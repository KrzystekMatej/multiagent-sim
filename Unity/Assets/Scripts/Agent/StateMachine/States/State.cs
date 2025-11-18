using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public abstract class State : AgentComponent
{
    [SerializeField] 
    private List<TransitionTemplate> transitionTemplates;
    [HideInInspector]
    public List<StateTransition> Transitions { get; private set; } = new List<StateTransition>();
    [SerializeField]
    protected AgentContext agent;
    protected AnimatorManager animator;

    public UnityEvent OnEnter, OnUpdate, OnExit;

    public override void Initialize(AgentContext agent)
    {
        this.agent = agent;
        animator = agent.Get<AnimatorManager>();
        BuildRuntimeTransitions();
    }

    public void PerformEnterActions()
    {
        HandleEnter();
        OnEnter?.Invoke();
    }

    private void BuildRuntimeTransitions()
    {
        Transitions.Clear();

        foreach (var template in transitionTemplates)
        {
            if (template == null) continue;

            Type transitionType = Type.GetType(template.Transition);
            Type stateType = Type.GetType(template.TargetState);

            if (transitionType == null || stateType == null)
            {
                Debug.LogWarning($"Type not found for transition '{template.Transition}' or state '{template.TargetState}'.");
                continue;
            }

            State targetState = GetComponent(stateType) as State;
            if (targetState == null)
            {
                Debug.LogWarning($"Target state '{template.TargetState}' not found on {name}.");
                continue;
            }

            try
            {
                ConstructorInfo constructor = transitionType.GetConstructor(new[] { typeof(State) });
                if (constructor == null)
                {
                    Debug.LogError($"Constructor (State) not found for transition '{template.Transition}'");
                    continue;
                }

                StateTransition transition = (StateTransition)constructor.Invoke(new object[] { targetState });
                Transitions.Add(transition);          
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to create transition '{template.Transition}': {e.Message}");
            }
        }
    }

    protected virtual void HandleEnter() { }


    public void PerformUpdateActions()
    {
        HandleUpdate();
        OnUpdate?.Invoke();
    }

    protected virtual void HandleUpdate() { }
    
    public void PerformExitActions()
    {
        HandleExit();
        OnExit?.Invoke();
    }

    protected virtual void HandleExit() { }
}
