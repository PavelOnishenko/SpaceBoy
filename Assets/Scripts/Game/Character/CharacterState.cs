using System.Linq;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [SerializeField] private BulletCreator bulletCreator;
    [SerializeField] private HeartsController heartsController;
    public bool isProtagonist;
    
    private CharacterDependentFeatures characterDependentFeatures;

    public bool IsDead => hp <= 0;

    protected Animator animator;

    private int hp;

    private readonly float damageCooldown = 0.1f;
    private float lastHitTime;

    private int InitialHp => characterDependentFeatures.InitialHp;

    private void Start()
    {
        var characterTransform = IntersceneState.GetCharacterDependentTransform(transform, isProtagonist);
        animator = characterTransform.gameObject.GetComponent<Animator>();
        characterDependentFeatures = characterTransform.gameObject.GetComponent<CharacterDependentFeatures>();
        hp = InitialHp;
    }

    public void SetDucked(bool isDucked) => 
        animator.SetBool(CharacterAnimationParamType.Ducked.ToString(), isDucked);

    public void Shoot()
    {
        Debug.Log("Shoot called");
        AudioManager.Instance.PlaySound("Shot");
        bulletCreator.CreateBullet();
    }

    public void Aim()
    {
        Debug.Log("Aim called");
        animator.SetTrigger(CharacterAnimationTriggerType.Aim.ToString());
    }

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