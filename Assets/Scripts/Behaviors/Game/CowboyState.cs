using UnityEngine;

public class CowboyState : MonoBehaviour
{
    [SerializeField] private string aimingAnimationName;

    public bool IsDead { get; private set; }
    public bool IsAiming { get; private set; }

    private Animator animator;

    public void PauseAiming()
    {
        animator.speed = 0;
        IsAiming = false;
    }

    public void StartAiming()
    {
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(aimingAnimationName))
        {
            animator.speed = 1;
        }
        else
        {
            animator.SetBool("IsAiming", true);
        }

        IsAiming = true;
    }

    public void Revive()
    {
        animator.speed = 1;
        IsAiming = false;
        animator.SetBool("IsAiming", false);
        IsDead = false;
        animator.SetBool("IsDead", false);
    }

    public void Die()
    {
        animator.speed = 1;
        IsDead = true;
        animator.SetBool("IsDead", true);
        var isPlayer = gameObject.name == "Spacegirl";
        if (GameInfo.Instance.State == GameState.Ongoing) GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}