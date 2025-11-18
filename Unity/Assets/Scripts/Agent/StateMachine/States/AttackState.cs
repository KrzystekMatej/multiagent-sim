using UnityEngine;

public class AttackState : State
{
    protected override void HandleEnter()
    {
        animator.PlayByName("Attack");
    }
}
