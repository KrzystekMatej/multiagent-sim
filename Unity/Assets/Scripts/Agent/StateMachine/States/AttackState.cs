using System.Runtime.InteropServices.WindowsRuntime;
using Unity.AppUI.UI;
using UnityEngine;

public class AttackState : State
{
    [SerializeField]
    private OverlapDetector attackDetector;
    private Rigidbody2D rigidBody;

    public override void Initialize(AgentContext agent)
    {
        base.Initialize(agent);
        rigidBody = agent.Get<Rigidbody2D>();
    }   

    protected override void HandleEnter()
    {
        rigidBody.linearVelocity = Vector3.zero;
        animator.PlayByName("Attack");
        animator.OnAnimationAction.AddListener(Attack);
    }

    public void Attack()
    {
        Vector2 direction = agent.Get<OrientationController>().CurrentOrientation;

        attackDetector.OriginOffset = direction * (attackDetector.Size.x / 2);
        attackDetector.Angle = VectorHelpers.GetVectorRadAngle(direction) * Mathf.Rad2Deg;
        int detectionCount = attackDetector.Detect(agent.transform.position);

        for (int i = 0; i < detectionCount; i++)
        {
            if (attackDetector.Colliders[i].gameObject == agent.gameObject) continue;
            AgentContext otherAgent = attackDetector.Colliders[i].GetComponent<AgentContext>();
            if (otherAgent == null) continue;

            otherAgent.Get<HealthManager>()?.ChangeHealth(-20);
        }
    }

    protected override void HandleExit()
    {
        animator.OnAnimationAction.RemoveListener(Attack);
    }

    public void OnDrawGizmos()
    {
        if (agent == null) return;
        attackDetector.DrawGizmos(agent.transform.position);
    }
}
