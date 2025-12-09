
public class SwimState : State
{
    protected override void HandleEnter()
    {
        animator.PlayByName("Swim");
    }
}
