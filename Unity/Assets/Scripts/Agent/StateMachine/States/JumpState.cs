
public class JumpState : State
{
    protected override void HandleEnter()
    {
        animator.PlayByName("Jump");
    }
}
