using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Game Parameters")]
public class GameParameters : ScriptableObject
{
    public float humanBulletSpeed;
    public float aiBulletSpeed;
    public float bulletDestructionTime;
    public float attackDelay_Brainman;
    public float attackDelay_Octopus;
    public float attackDelay_Lizard;
}