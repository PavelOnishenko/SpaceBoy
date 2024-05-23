using UnityEngine;

public class CowboyState : MonoBehaviour
{
    [SerializeField] private string protagobistGameObjectName = "Spacegirl";
    [SerializeField] private string isDeadAnimatorBoolName = "IsDead";
    
    // todo remove redundant Animator bool param IsAiming from Protagonist and fix animation state machine

    public bool IsDead { get; private set; }

    protected Animator animator;

    public void Revive()
    {
        // todo do we need this speed setting? (and another place below)
        animator.speed = 1;
        IsDead = false;
        animator.SetBool(isDeadAnimatorBoolName, false);
    }

    public void Die()
    {
        animator.speed = 1;
        IsDead = true;
        animator.SetBool(isDeadAnimatorBoolName, true);
        var isPlayer = gameObject.name == protagobistGameObjectName;
        if (GameInfo.Instance.State == GameState.Ongoing) 
            GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
    }

    private void Start() => animator = GetComponent<Animator>();
}