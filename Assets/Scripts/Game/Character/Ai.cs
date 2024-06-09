using System.Collections;
using UnityEngine;

public class Ai : MonoBehaviour
{

    [SerializeField] private float delayBeforeAttackSeconds = 0.2f;

    private CharacterState state;
    private Coroutine attackCoroutine;

    private void Start()
    {
        state = GetComponent<CharacterState>();
    }

    public void AttackAfterDelay()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        attackCoroutine = StartCoroutine(AttackAfterDelayCoroutine());
    }

    IEnumerator AttackAfterDelayCoroutine()
    {
        //state.Calm();
        while (GameInfo.Instance.State is not GameState.Ongoing)
        {
            yield return null;
        }
        yield return new WaitForSeconds(delayBeforeAttackSeconds);
        state.Aim();
        attackCoroutine = null;
    }
}