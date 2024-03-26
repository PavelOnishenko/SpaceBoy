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
        GameInfo.Instance.State = GameInfo.GameState.Finished;
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Die");
    }
}
