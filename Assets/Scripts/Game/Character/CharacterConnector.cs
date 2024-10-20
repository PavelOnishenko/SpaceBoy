using UnityEngine;

public class CharacterConnector : MonoBehaviour
{
    [SerializeField] private CharacterState characterState;

    public void Shoot()
    {
        characterState.Shoot();
    }
}
