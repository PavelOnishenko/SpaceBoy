using UnityEngine;

public class ShootButtonBehavior : MonoBehaviour
{
    [SerializeField] private CharacterState stateController;

    private void Update()
    {
        var protagonist = GameInfo.Instance?.Protagonist;
        if (stateController == null && protagonist != null) 
            stateController = protagonist.GetComponent<CharacterState>();
    }

    public void HandleClick() => stateController.Aim();
}