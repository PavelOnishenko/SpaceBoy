using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
    [SerializeField] private string protagonistGameObjectName = "Spacegirl";
    [SerializeField] private string isDeadAnimatorBoolName = "IsDead";
    [SerializeField] private BulletCreator bulletCreator;
    
    public bool IsDead { get; private set; }

    protected Animator animator;

    private void Start() => animator = GetComponent<Animator>();

    public void Shoot()
    {
        bulletCreator.CreateBullet();
        animator.SetTrigger("Shoot");
    }

    public void Aim()
    {
        animator.SetTrigger("Aim");
    }

    public void Revive()
    {
        IsDead = false;
        animator.SetBool(isDeadAnimatorBoolName, false);
    }

    public void Die()
    {
        IsDead = true;
        animator.SetBool(isDeadAnimatorBoolName, true);
        var isPlayer = gameObject.name == protagonistGameObjectName;
        if (GameInfo.Instance.State == GameState.Ongoing) 
            GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
    }
}