using UnityEngine;

public class IdleState : State
{
    private Rigidbody2D rigidBody;

    public override void Initialize(AgentContext agent)
    {
        base.Initialize(agent);
        rigidBody = agent.Get<Rigidbody2D>();
    }   

    protected override void HandleEnter()
    {
        animator.PlayByName("Idle");
        rigidBody.linearVelocity = Vector2.zero;
    }

    protected override void HandleUpdate() { }

    protected override void HandleExit() { }
}
