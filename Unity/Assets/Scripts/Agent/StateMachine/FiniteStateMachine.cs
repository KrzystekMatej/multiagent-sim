using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class FiniteStateMachine : AgentComponent
{
    [field: SerializeField]
    public State InitialState { get; private set; }
    public State CurrentState { get; private set; }
    [SerializeField]
    private AgentContext agent;
    private State[] states;

    public UnityEvent<State, State> OnTransition;

    public override void Initialize(AgentContext agent)
    {
        InitialState = InitialState ? InitialState : GetComponent<IdleState>();
        states = GetComponents<State>();
        this.agent = agent;
    }  

    private void Start()
    {
        foreach (State s in states)
        {
            foreach (StateTransition t in s.Transitions)
            {
               t.Initialize(agent);
            }
        }
        CurrentState = InitialState;
        CurrentState.PerformEnterActions();
    }

    private void Update()
    {
        CurrentState.PerformUpdateActions();

        StateTransition triggered = CurrentState.Transitions.FirstOrDefault(t => t.IsTriggered(agent));

        if (triggered != null)
        {
            PerformTransition(triggered, agent);
        }
    }

    private void PerformTransition(StateTransition triggered, AgentContext agent)
    {
        if (triggered.Target)
        {
            CurrentState.PerformExitActions();

            OnTransition?.Invoke(CurrentState, triggered.Target);

            triggered.PerformTransitionAction(agent);
            CurrentState = triggered.Target;
            CurrentState.PerformEnterActions();
        }
    }
}

