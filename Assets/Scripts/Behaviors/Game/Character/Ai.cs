using System.Collections;
using UnityEngine;

public class Ai : MonoBehaviour
{

    [SerializeField] private float delayBeforeAttackSeconds = 0.1f;

    private CharacterStateController state;

    public void AttackAfterDelay()
    {
        StartCoroutine(AttackAfterDelayCoroutine());
        state = GetComponent<CharacterStateController>();
    }

    IEnumerator AttackAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeAttackSeconds);
        state.Aim();
    }
}
