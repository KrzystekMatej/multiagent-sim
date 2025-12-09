using UnityEngine;

public class RollState : State
{
    [field: SerializeField]
    public float RollSpeed { get; set; }
    private Rigidbody2D rigidBody;

    public override void Initialize(AgentContext agent)
    {
        base.Initialize(agent);
        rigidBody = agent.Get<Rigidbody2D>();
    }


    protected override void HandleEnter()
    {
        animator.PlayByName("Roll");
        animator.OnAnimationAction.AddListener(Rolling);
        rigidBody.linearVelocity = new Vector2(agent.Get<OrientationController>().CurrentOrientation.x * RollSpeed, 0);
    }

    private void Rolling()
    {
        rigidBody.linearVelocity = Vector2.zero;
    }

    protected override void HandleExit()
    {
        animator.OnAnimationAction.RemoveListener(Rolling);
    }
}
