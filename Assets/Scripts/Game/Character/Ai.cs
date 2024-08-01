using Assets.Scripts.Edtitor;
using Assets.Scripts.Menu;
using System;
using System.Collections;
using UnityEngine;

public class Ai : MonoBehaviour, IDesignerConfigurable
{

    [SerializeField] private float delayBeforeAttackSeconds = 0.2f;

    private CharacterState state;
    private Coroutine attackCoroutine;

    private void Start() => state = GetComponent<CharacterState>();

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
        yield return new WaitForSeconds(delayBeforeAttackSeconds + 10000); // TODO ROLLBACK + 10000
        state.Aim();
        attackCoroutine = null;
    }



    #region FOR EDITOR

    public void ApplyParameters()
    {
        var gameParameters = GameParametersManager.Instance.gameParameters;
        if (gameParameters != null)
            delayBeforeAttackSeconds = gameObject.name switch
            {
                var name when name.StartsWith(CharacterType.Brainman.ToString()) => gameParameters.attackDelay_Brainman,
                var name when name.StartsWith(CharacterType.Lizard.ToString()) => gameParameters.attackDelay_Lizard,
                var name when name.StartsWith(CharacterType.Octopus.ToString()) => gameParameters.attackDelay_Octopus,
                _ => throw new ArgumentException($"Invalid character type [{gameObject.name}].")
            };
        else
            Debug.LogError("ASSIGN GAME PARAMS!!!");
    }

    #endregion
}