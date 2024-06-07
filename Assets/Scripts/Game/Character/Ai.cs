using System.Collections;
using UnityEngine;

public class Ai : MonoBehaviour
{

    [SerializeField] private float delayBeforeAttackSeconds = 5f;

    private CharacterState state;

    public void AttackAfterDelay()
    {
        StartCoroutine(AttackAfterDelayCoroutine());
        state = GetComponent<CharacterState>();
    }

    IEnumerator AttackAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeAttackSeconds);
        state.Aim();
    }
}