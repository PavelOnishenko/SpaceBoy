using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1000f;
    [SerializeField] private float destroyTime = 3f;

    private Rigidbody2D rigidbody2DComponent;

    void Start()
    {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
        rigidbody2DComponent.linearVelocity = transform.up * movementSpeed;
        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var go = other.gameObject;
        if (go.IsParentWithName("Protagonist"))
            go.GetComponentFromParentByName<CharacterState>("Protagonist").GetHit();
        else if (go.IsParentWithName("Enemy"))
            go.GetComponentFromParentByName<CharacterState>("Enemy").GetHit();
        Destroy(gameObject);
    }
}