using System;
using UnityEngine;

public class RunState : WalkState
{
    protected override void HandleEnter()
    {
        animator.PlayByName("Run");
    }
}