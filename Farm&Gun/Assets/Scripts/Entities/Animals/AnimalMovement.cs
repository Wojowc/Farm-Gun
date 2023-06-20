public class AnimalMovement : Movement
{
    private bool canMove = true;

    public override void DisableMovement()
    {
        canMove = false;
        //agent.enabled = false;
    }

    public override void EnableMovement()
    {
        canMove = true;
        //agent.enabled = true;
    }
}