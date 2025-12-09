
using UnityEngine;

public class HurtState : State
{
    protected override void HandleEnter()
    {
        animator.PlayByName("Hurt");
        agent.Get<Rigidbody2D>().linearVelocity = Vector3.zero;
    }
}
