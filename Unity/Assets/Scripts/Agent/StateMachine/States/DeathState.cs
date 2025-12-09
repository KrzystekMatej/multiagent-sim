using System.Collections;
using UnityEngine;

public class DeathState : State
{
    [SerializeField]
    private float deathDuration = 2f;
    private Rigidbody2D rigidBody;

    public override void Initialize(AgentContext agent)
    {
        base.Initialize(agent);
        rigidBody = agent.Get<Rigidbody2D>();
    }

    protected override void HandleEnter()
    {
        animator.PlayByName("Death");
        animator.OnAnimationComplete.AddListener(CompleteTheDeath);
    }

    private void CompleteTheDeath()
    {
        animator.OnAnimationComplete.RemoveListener(CompleteTheDeath);
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(deathDuration);
        PerformExitActions();
    }

    protected override void HandleUpdate()
    {
        rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
    }

    protected override void HandleExit() { }
}
