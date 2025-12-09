using UnityEngine;

public class BuildState : State
{
    protected override void HandleEnter()
    {
        animator.PlayByName("Build");
        agent.Get<Rigidbody2D>().linearVelocity = Vector3.zero;
    }
}
