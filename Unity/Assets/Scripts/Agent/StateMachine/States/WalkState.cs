using System;
using UnityEngine;

public class WalkState : State
{
    [field: SerializeField]
    public float MaxAcceleration { get; set; }
    [field: SerializeField]
    public float MaxSpeed { get; set; }


    protected AgentInputProvider inputProvider;
    private Rigidbody2D rigidBody;

    public override void Initialize(AgentContext agent)
    {
        base.Initialize(agent);
        rigidBody = agent.Get<Rigidbody2D>();
        inputProvider = agent.Get<AgentInputProvider>();
    }

    protected override void HandleEnter()
    {
        animator.PlayByName("Walk");
    }

    protected override void HandleUpdate()
    {
        Vector2 input = inputProvider.InputData.MoveInput;
        Vector2 acceleration = input * MaxAcceleration;

        rigidBody.linearVelocity += acceleration * Time.deltaTime;
        rigidBody.linearVelocity = Vector2.ClampMagnitude(rigidBody.linearVelocity, MaxSpeed);
    }
}