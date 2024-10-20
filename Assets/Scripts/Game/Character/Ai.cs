using System.Collections;
using UnityEngine;

public class Ai : MonoBehaviour
{

    [SerializeField] private float delayBeforeAttackSeconds = 0.2f;

    private CharacterState state;
    private Coroutine attackCoroutine;

    private void Start() => state = GetComponentInParent<CharacterState>();

    public void AttackAfterDelay()
    {
        if (!gameObject.activeSelf)
            return;
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(AttackAfterDelayCoroutine());
    }

    IEnumerator AttackAfterDelayCoroutine()
    {
        while (GameInfo.Instance.State is not GameState.Ongoing) yield return null;
        yield return new WaitForSeconds(delayBeforeAttackSeconds);
        state.Aim();
        attackCoroutine = null;
    }
}