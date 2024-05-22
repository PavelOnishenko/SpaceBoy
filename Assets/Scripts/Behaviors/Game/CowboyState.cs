using UnityEngine;

public class CowboyState : MonoBehaviour
{
    [SerializeField] private string mainCharacterGameObjectName = "Spacegirl";
    [SerializeField] private string isAimingAnimatorBoolName = "IsAiming";
    [SerializeField] private string isDeadAnimatorBoolName = "IsDead";
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(aimingAnimationName))
            animator.speed = 1;
        else
            animator.SetBool(isAimingAnimatorBoolName, true);

        IsAiming = true;
    }

    public void Revive()
    {
        animator.speed = 1;
        IsAiming = false;
        animator.SetBool(isAimingAnimatorBoolName, false);
        IsDead = false;
        animator.SetBool(isDeadAnimatorBoolName, false);
    }

    public void Die()
    {
        animator.speed = 1;
        IsDead = true;
        animator.SetBool(isDeadAnimatorBoolName, true);
        var isPlayer = gameObject.name == mainCharacterGameObjectName;
        if (GameInfo.Instance.State == GameState.Ongoing) 
            GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
    }

    private void Start() => animator = GetComponent<Animator>();
}