using System.Collections;
using UnityEngine;

public class Ai : MonoBehaviour
{

    [SerializeField] private float delayBeforeAttackSeconds = 0.5f;

    private BaseStateController state;

    public void AttackAfterDelay()
    {
        StartCoroutine(AttackAfterDelayCoroutine());
        state = GetComponent<BaseStateController>();
    }

    IEnumerator AttackAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delayBeforeAttackSeconds);
        state.Aim();
    }
}
