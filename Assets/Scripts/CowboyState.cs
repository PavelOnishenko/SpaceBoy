using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyState : MonoBehaviour
{
    public bool IsDead => isDead;

    private bool isDead;

    public void Die()
    {
        isDead = true;
        var isPlayer = gameObject.name == "Cowboy";
        if (GameInfo.Instance.State == GameInfo.GameState.Ongoing)
        {
            GameInfo.Instance.State = isPlayer ? GameInfo.GameState.PlayerDead : GameInfo.GameState.PlayerWon;
        }
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Die");
    }
}
