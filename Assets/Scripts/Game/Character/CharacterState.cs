using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private string protagonistGameObjectName = "Spacegirl";
    [SerializeField] private string isDeaParamName = "IsDead";
    [SerializeField] private BulletCreator bulletCreator;
    [SerializeField] private int initialHp = 2;
    [SerializeField] private string heartContainerNamePrefix;

    private GameObject characterContainer;
    private GameObject heartsContainer;
    private HeartsController heartsController;
    private Ai ai;

    public bool IsDead => hp <= 0;

    private int hp;

    protected Animator animator;

    private void Start()
    {
        hp = initialHp;
        animator = GetComponent<Animator>();
        characterContainer = transform.parent.gameObject;
        heartsContainer = GameObject.Find($"{heartContainerNamePrefix}HeartContainer");
        heartsController = heartsContainer.GetComponent<HeartsController>();
        ai = GetComponent<Ai>();
    }

    public void Shoot()
    {
        bulletCreator.CreateBullet();
    }

    public void Aim()
    {
        animator.SetTrigger("Aim");
    }

    public void Revive()
    {
        hp = initialHp;
        heartsController.SetHp(hp);
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