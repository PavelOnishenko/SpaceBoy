using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private BulletCreator bulletCreator;
    [SerializeField] private int initialHp = 2;
    [SerializeField] private string heartContainerNamePrefix;
    public bool IsDead => hp <= 0;

    protected Animator animator;

    private GameObject heartsContainer;
    private HeartsController heartsController;
    private int hp;

    private void Start()
    {
        hp = initialHp;
        animator = GetComponent<Animator>();
        heartsContainer = GameObject.Find($"{heartContainerNamePrefix}HeartContainer");
        heartsController = heartsContainer.GetComponent<HeartsController>();
    }

    public void Shoot() => bulletCreator.CreateBullet();

    public void Aim() => animator.SetTrigger(CharacterAnimationTriggerType.Aim.ToString());

    public void Revive()
    {
        hp = initialHp;
        heartsController.SetHp(hp);
        animator.SetBool(CharacterAnimationParamType.IsDead.ToString(), false);
    }

    public void GetHit()
    {
        hp--;
        heartsController.SetHp(hp);
        if(IsDead)
        {
            animator.SetBool(CharacterAnimationParamType.IsDead.ToString(), true);
            var selectedProtagonistString = IntersceneState.Instance.SelectedProtagonist.ToString();
            var isPlayer = gameObject.name == selectedProtagonistString;
            if (GameInfo.Instance.State == GameState.Ongoing)
                GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
        }
        else
        {
            animator.SetTrigger(CharacterAnimationTriggerType.GetHit.ToString());
        }
    }
}