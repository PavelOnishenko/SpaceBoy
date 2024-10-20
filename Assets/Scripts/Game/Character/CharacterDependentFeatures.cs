using UnityEngine;

public class CharacterDependentFeatures : MonoBehaviour
{
    [SerializeField] private int initialHp;
    [SerializeField] private float attackDelay;

    public int InitialHp => initialHp;
    public float AttackDelay => attackDelay;
}
