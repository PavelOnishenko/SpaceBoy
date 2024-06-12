using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1000f;
    [SerializeField] private float destroyTime = 3f;

    private Rigidbody2D rigidbody2DComponent;

    void Start()
    {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
        rigidbody2DComponent.velocity = transform.up * movementSpeed;
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var protagonistIsHit = other.gameObject.IsParentWithName("ProtagonistContainer");
        string characterName;
        if (protagonistIsHit)
        {
            characterName = IntersceneState.Instance.SelectedProtagonist.ToString();
        }
        else
        {
            characterName = GameInfo.Instance.Enemy.name;
        }
        other.gameObject.GetComponentFromParentByName<CharacterState>(characterName)?.GetHit();
        Destroy(gameObject);
    }
}