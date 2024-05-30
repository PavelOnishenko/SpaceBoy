using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private string protagonistGameObjectName = "Spacegirl";
    [SerializeField] private string isDeaParamName = "IsDead";
    [SerializeField] private BulletCreator bulletCreator;
    [SerializeField] private int initialHp = 2;
    [SerializeField] private GameObject heartsContainer;

    private HeartsController heartsController;

    public bool IsDead => hp <= 0;

    private int hp;

    protected Animator animator;

    private void Start()
    {
        hp = initialHp;
        animator = GetComponent<Animator>();
        heartsController = heartsContainer.GetComponent<HeartsController>();
    }

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
        hp = initialHp;
        animator.SetBool(isDeaParamName, false);
    }

    public void GetHit()
    {
        hp--;
        heartsController.SetHp(hp);
        if(IsDead)
        {
            animator.SetBool(isDeaParamName, true);
            var isPlayer = gameObject.name == protagonistGameObjectName;
            if (GameInfo.Instance.State == GameState.Ongoing)
                GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
        }
        else
        {
            animator.SetTrigger("GetHit");
        }
    }
}