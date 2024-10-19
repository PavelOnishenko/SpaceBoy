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

    private readonly float damageCooldown = 0.1f;
    private float lastHitTime;
    private bool isDucked;

    private void Start()
    {
        hp = initialHp;
        animator = GetComponent<Animator>();
        heartsContainer = GameObject.Find($"{heartContainerNamePrefix}HeartContainer");
        heartsController = heartsContainer.GetComponent<HeartsController>();
    }

    public void SetDucked(bool isDucked)
    {
        this.isDucked = isDucked;
        animator.SetBool(CharacterAnimationParamType.Ducked.ToString(), isDucked);
    }

    public void Shoot()
    {
        AudioManager.Instance.PlaySound("Shot");
        bulletCreator.CreateBullet();
    }

    public void Aim() => animator.SetTrigger(CharacterAnimationTriggerType.Aim.ToString());

    public void Revive()
    {
        hp = initialHp;
        heartsController.SetHp(hp);
        animator.SetBool(CharacterAnimationParamType.IsDead.ToString(), false);
    }

    public void GetHit()
    {
        if (Time.time - lastHitTime < damageCooldown) 
            return;

        lastHitTime = Time.time;

        hp--;
        heartsController.SetHp(hp);
        if(IsDead)
        {
            AudioManager.Instance.PlaySound("Death");
            GetDead();
        }
        else
        {
            AudioManager.Instance.PlaySound("GetHit");
            animator.SetTrigger(CharacterAnimationTriggerType.GetHit.ToString());
        }
    }

    private void GetDead()
    {
        animator.SetBool(CharacterAnimationParamType.IsDead.ToString(), true);
        var isPlayer = gameObject.name == IntersceneState.Instance.SelectedProtagonist.ToString();
        if (GameInfo.Instance.State == GameState.Ongoing)
            GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
    }
}