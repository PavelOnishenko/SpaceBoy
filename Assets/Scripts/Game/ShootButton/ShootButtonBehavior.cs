using UnityEngine;

public class ShootButtonBehavior : MonoBehaviour
{
    [SerializeField] private CharacterState stateController;

    private void Update()
    {
        var protagonistState = GameInfo.Instance?.protagonistState;
        if (stateController == null && protagonistState != null) 
            stateController = protagonistState;
    }

    public void HandleClick() => stateController.Aim();
}