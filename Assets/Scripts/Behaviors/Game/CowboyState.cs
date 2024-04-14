using UnityEngine;

public class CowboyState : MonoBehaviour
{
    [SerializeField] private string aimingAnimationName;
    [SerializeField] private string idleAnimationName;

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
        IsDead = false;
    }

    public void Die()
    {
        IsDead = true;
        var isPlayer = gameObject.name == "Cowboy";
        if (GameInfo.Instance.State == GameState.Ongoing) GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
}