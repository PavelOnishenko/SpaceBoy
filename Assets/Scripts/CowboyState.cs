using UnityEngine;

public class CowboyState : MonoBehaviour
{
    public bool IsDead => isDead;

    private bool isDead;
    
    private static readonly int die = Animator.StringToHash("Die");

    public void Die()
    {
        isDead = true;
        var isPlayer = gameObject.name == "Cowboy";
        if (GameInfo.Instance.State == GameState.Ongoing) GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
        var animator = GetComponent<Animator>();
        animator.SetTrigger(die);
    }
}
