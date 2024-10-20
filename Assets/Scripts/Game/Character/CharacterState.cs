using System.Linq;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private BulletCreator bulletCreator;
    [SerializeField] private HeartsController heartsController;
    [SerializeField] private bool isProtagonist;
    
    private CharacterDependentFeatures characterDependentFeatures;

    public bool IsDead => hp <= 0;

    protected Animator animator;

    private int hp;

    private readonly float damageCooldown = 0.1f;
    private float lastHitTime;

    private int InitialHp => characterDependentFeatures.InitialHp;

    private void Start()
    {
        // todo refactor 3 times
        var selectedCharacterType = isProtagonist ? IntersceneState.Instance.SelectedProtagonist : IntersceneState.Instance.SelectedEnemy;
        var characterTransform = transform.Cast<Transform>().Single(transform => transform.gameObject.name.Contains(selectedCharacterType.ToString()));
        characterDependentFeatures = characterTransform.gameObject.GetComponent<CharacterDependentFeatures>();
        hp = InitialHp;
        animator = characterTransform.gameObject.GetComponent<Animator>();
    }

    public void SetDucked(bool isDucked) => 
        animator.SetBool(CharacterAnimationParamType.Ducked.ToString(), isDucked);

    public void Shoot()
    {
        AudioManager.Instance.PlaySound("Shot");
        bulletCreator.CreateBullet();
    }

    public void Aim() => animator.SetTrigger(CharacterAnimationTriggerType.Aim.ToString());

    public void Revive()
    {
        hp = InitialHp;
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
        var isPlayer = gameObject.name == "Protagonist";
        if (GameInfo.Instance.State == GameState.Ongoing)
            GameInfo.Instance.State = isPlayer ? GameState.PlayerDead : GameState.PlayerWon;
    }
}