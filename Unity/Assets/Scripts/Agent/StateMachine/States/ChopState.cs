using UnityEngine;

public class ChopState : State
{
    protected override void HandleEnter()
    {
        animator.PlayByName("Chop");
        agent.Get<Rigidbody2D>().linearVelocity = Vector3.zero;
    }
}
