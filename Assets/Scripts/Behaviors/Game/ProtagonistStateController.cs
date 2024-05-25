public class ProtagonistStateController : BaseStateController
{
    public void Aim()
    {
        animator.SetTrigger("Aim");
    }
}
