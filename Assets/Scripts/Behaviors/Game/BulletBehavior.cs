using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15f;
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
        Destroy(gameObject);
        var rootTransform = other.transform.root;
        var cowboyState = rootTransform.gameObject.GetComponentInChildren<BaseStateController>();
        if (cowboyState != null) cowboyState.Die();
    }
}