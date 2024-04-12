using UnityEngine;

public class CowboyState : MonoBehaviour
{
    public bool IsDead => isDead;
    public bool IsAiming { get; private set; }

    private bool isDead;
    private Animator animator;

    private static readonly int isDeadParameterId = Animator.StringToHash("IsDead");

    public void StopAiming() => IsAiming = false;

    public void StartAiming()
    {
        animator.Play("SpaceGirl_Attack");
        IsAiming = true;
    }

    public void Revive()
    {
        isDead = false;
        animator.SetBool(isDeadParameterId, false);
    }

    public void Die()
    {
        isDead = true;
        var isPlayer = gameObject.name == "Cowboy";
        if (GameInfo.Instance.State == GameState.Ongoing) GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
        animator.SetBool(isDeadParameterId, true);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("SpaceGirl_Idle");
    }
}