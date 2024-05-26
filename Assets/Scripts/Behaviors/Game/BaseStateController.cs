using UnityEngine;

// todo �������� ������������ �� ����������
// todo ��������� �� ���������� CharacterStateController
// todo ����� ������ ���� ������ MonoBeh �� ���� GameObject, � ������ ��� ������������ ������� ������, �� MonoBeh
public class BaseStateController : MonoBehaviour
{
    [SerializeField] private string protagobistGameObjectName = "Spacegirl";
    [SerializeField] private string isDeadAnimatorBoolName = "IsDead";
    [SerializeField] private BulletCreator bulletCreator;
    
    public bool IsDead { get; private set; }

    protected Animator animator;

    private void Start() => animator = GetComponent<Animator>();

    public void Revive()
    {
        // todo do we need this speed setting? (and another place below)
        animator.speed = 1;
        IsDead = false;
        animator.SetBool(isDeadAnimatorBoolName, false);
    }

    public void Die()
    {
        animator.speed = 1;
        IsDead = true;
        animator.SetBool(isDeadAnimatorBoolName, true);
        var isPlayer = gameObject.name == protagobistGameObjectName;
        if (GameInfo.Instance.State == GameState.Ongoing) 
            GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
    }

    public void CreateBullet() => bulletCreator.CreateBullet();
}