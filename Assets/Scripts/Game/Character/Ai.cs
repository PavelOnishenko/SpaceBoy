using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class Ai : MonoBehaviour
{
    private float delayBeforeAttackSeconds;
    private CharacterState state;
    private Coroutine attackCoroutine;

    private void Start()
    {
        delayBeforeAttackSeconds = GetComponent<CharacterDependentFeatures>().AttackDelay;
        state = GetComponentInParent<CharacterState>();
    }

    public void AttackAfterDelay()
    {
        if (!gameObject.activeSelf)
            return;
        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(AttackAfterDelayCoroutine());
    }

    IEnumerator AttackAfterDelayCoroutine()
    {
        while (GameInfo.Instance.State is not GameState.Ongoing) 
            yield return null;
        Debug.Log($"Starting DELAY=[{delayBeforeAttackSeconds}]");
        var time1 = DateTime.Now;
        yield return new WaitForSeconds(delayBeforeAttackSeconds);
        var time2 = DateTime.Now;
        Debug.Log($"Finished DELAY. Length: [{(time2-time1).TotalSeconds}] sec.");
        state.Aim();
        attackCoroutine = null;
    }
}