using UnityEngine;

public class DuckButton : MonoBehaviour
{
    [SerializeField] CharacterState characterState;

    public void Duck()
    {
        characterState.SetDucked(true);
    }

    public void StandUp()
    {
        characterState.SetDucked(false);
    }
}
