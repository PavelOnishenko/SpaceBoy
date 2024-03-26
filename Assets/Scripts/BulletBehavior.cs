using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15f;
    
    private Rigidbody2D rigidbody2DComponent;
    
    void Start()
    {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
        rigidbody2DComponent.velocity = transform.up * movementSpeed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        var otherGameObject = other.gameObject;
        CowboyState cowboyState = null;
        if(otherGameObject.name is "Cowboy" or "Enemy") cowboyState = otherGameObject.GetComponent<CowboyState>();
        if (otherGameObject.name is "CowboyHand" or "EnemyHand") cowboyState = otherGameObject.GetComponentInParent<CowboyState>();
        if (cowboyState != null) cowboyState.Die();
    }
}
