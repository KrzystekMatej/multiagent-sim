using UnityEngine;

public class DigState : State
{
    protected override void HandleEnter()
    {
        animator.PlayByName("Dig");
        agent.Get<Rigidbody2D>().linearVelocity = Vector3.zero;
    }
}
