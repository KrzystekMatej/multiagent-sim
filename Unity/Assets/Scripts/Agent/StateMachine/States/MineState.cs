
public class MineState : State
{
    protected override void HandleEnter()
    {
        animator.PlayByName("Mine");
    }
}
