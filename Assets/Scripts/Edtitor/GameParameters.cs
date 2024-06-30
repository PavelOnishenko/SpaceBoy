using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "Game Parameters")]
public class GameParameters : ScriptableObject
{
    public float humanBulletSpeed;
    public float aiBulletSpeed;
    public float bulletDestructionTime;
}