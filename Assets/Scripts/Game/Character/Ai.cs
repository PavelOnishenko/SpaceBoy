using UnityEngine;

public class Ai : MonoBehaviour
{
    private float delayBeforeAttackSeconds;
    private CharacterState state;
    private float attackTimer;

    private void Start()
    {
        delayBeforeAttackSeconds = GetComponent<CharacterDependentFeatures>().AttackDelay;
        Debug.Log($"Delay: [{delayBeforeAttackSeconds}].");
        state = GetComponentInParent<CharacterState>();
        attackTimer = delayBeforeAttackSeconds;
    }

    private void Update()
    {
        if (GameInfo.Instance.State != GameState.Ongoing)
            return;

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0f)
        {
            state.Aim();
            attackTimer = delayBeforeAttackSeconds;
        }
    }
}