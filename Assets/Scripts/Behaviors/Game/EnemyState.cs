using UnityEngine;

public class EnemyState : ProtagonistState
{
    [SerializeField] private string isAimingAnimatorBoolName = "IsAiming";
    [SerializeField] private string aimingAnimationName;

    public bool IsAiming { get; private set; }

    public void PauseAiming()
    {
        animator.speed = 0;
        IsAiming = false;
    }

    public void StartAiming()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(aimingAnimationName))
            animator.speed = 1;
        else
            animator.SetBool(isAimingAnimatorBoolName, true);

        IsAiming = true;
    }
}