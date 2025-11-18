using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorManager : AgentComponent
{
    private Animator animator;
    private string currentStateName;

    public UnityEvent OnAnimationComplete;
    public UnityEvent OnAnimationAction;

    public override void Initialize(AgentContext agent)
    {
        animator = agent.Get<Animator>(false);
        animator.Play(currentStateName, -1, 0f);
    }   

    public void PlayByName(string state)
    {
        currentStateName = state;
        animator.Play(currentStateName, -1, 0f);
    }
    public void Enable()
    {
        animator.enabled = true;
    }

    public void Disable()
    {
        animator.enabled = false;
    }

    public void ResetEvents()
    {
        OnAnimationAction.RemoveAllListeners();
        OnAnimationComplete.RemoveAllListeners();
    }

    public void InvokeAnimationAction()
    {
        OnAnimationAction?.Invoke();
    }

    public void InvokeAnimationComplete()
    {
        OnAnimationComplete?.Invoke();
    }
}
